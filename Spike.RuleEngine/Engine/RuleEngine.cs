using Spike.RuleEngine.Contracts;
using Spike.RuleEngine.Engine;
using Spike.RuleEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spike.Engine.RuleEngine
{

    public abstract class RuleEngine <T, Y> 
        where T : IDataModel
    {

        private List<string> _ruleWhitelist;

        private List<Rule<T, Y>> _rules;
        public List<Rule<T, Y>> Rules => _rules ?? (_rules  = new List<Rule<T, Y>>());

        public abstract void InitializeRules();


        public RuleEngine(List<string> ruleWhitelist = null)
        {
            _ruleWhitelist = ruleWhitelist;
            InitializeRules();
        }

        public abstract void SetVariables(T details, ref Y variables);

        public void AddRule(Enum rule, Func<T, Y, RuleStatus> logic, bool requiresConsent = false) // , Enum grouping
        {
            var newRule = new Rule<T, Y>(rule, logic, requiresConsent);

            if (_ruleWhitelist != null && !_ruleWhitelist.Contains(newRule?.Code))
            {
                newRule.Disabled = true;
            }

            Rules.Add(newRule);
        }

        public Result ApplyRules(T Data, bool hasSubjectConsent)
        {
            var genericType = typeof(Y);
            var variables = (Y)Activator.CreateInstance(genericType);

            SetVariables(Data, ref variables);


            foreach (var rule in Rules.OrderBy(r => r.Grouping))
            {
                if (!rule.Disabled)
                {
                    if (rule.RequiresConsent && !hasSubjectConsent)
                    {
                        rule.Skip();
                        continue;
                    }

                    rule.Run(Data, variables);
                }              
            }

            var failedCount = Rules.Count(r => r.Status == RuleStatus.Failed);
            var ignoredCount = Rules.Count(r => r.Status == RuleStatus.Ignored);
            var succeededCount = Rules.Count(r => r.Status == RuleStatus.Succeeded);
            var skippedCount = Rules.Count(r => r.Status == RuleStatus.Skipped);
            var applicableCount = failedCount + succeededCount + skippedCount;

            int percentageSucceeded = (int)Math.Round((double)(100 * succeededCount) / applicableCount);
            int percentageFailed = (int)Math.Round((double)(100 * failedCount) / applicableCount);
            int percentageSkipped = (int)Math.Round((double)(100 * skippedCount) / applicableCount);
                        
            return new Result
            {
                TotalApplicableRules = applicableCount,
                TotalIgnoredRules = ignoredCount,
                PercentageSucceeded = percentageSucceeded,                             
                PercentageFailed = percentageFailed,
                PercentageSkipped = percentageSkipped,
                FailedRules = Rules.Where(r => r.Status == RuleStatus.Failed).Select(r => r.Name).ToList()
            };
        }
    }
}
