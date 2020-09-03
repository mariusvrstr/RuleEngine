using Spike.RuleEngine.Contracts;
using Spike.RuleEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spike.Engine.RuleEngine
{

    public abstract class RuleEngine <T> 
        where T : IDataModel
    {

        private List<string> _ruleWhitelist;

        private List<Rule<T>> _rules;
        public List<Rule<T>> Rules => _rules ?? (_rules  = new List<Rule<T>>());

        public abstract void InitializeRules();


        public RuleEngine(List<string> ruleWhitelist = null)
        {
            _ruleWhitelist = ruleWhitelist;
            InitializeRules();
        }

        public void AddRule(Enum rule, Func<T, RuleStatus> logic) // , Enum grouping
        {
            var newRule = new Rule<T>(rule, logic);

            if (_ruleWhitelist != null && !_ruleWhitelist.Contains(newRule?.Code))
            {
                newRule.Disabled = true;
            }

            Rules.Add(newRule);
        }

        public Result ApplyRules(T Data)
        {
            foreach (var rule in Rules.OrderBy(r => r.Grouping))
            {
                if (!rule.Disabled)
                {
                    rule.Run(Data);
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
