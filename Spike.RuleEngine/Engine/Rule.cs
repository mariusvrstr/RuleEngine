using Spike.RuleEngine.Contracts;
using Spike.RuleEngine.Engine;
using System;
using System.Runtime.CompilerServices;

namespace Spike.Engine.RuleEngine
{
    public class Rule<T, Y> 
        where T : IDataModel
    {
        public string Code { get; set; }

        public Func<T, Y, RuleStatus> Logic;

        public bool RequiresConsent { get; set; }

        public string Grouping { get; private set; }

        public RuleStatus Status { get; private set; }

        public string Name { get; set; }

        private bool _disabled;
        public bool Disabled 
        {
            get
            {
                return _disabled;
            }
            set
            {
                if (value)
                {
                    Status = RuleStatus.Ignored;
                }
                
                _disabled = value;
            }
        }

        public Rule(Enum rule, Func<T, Y, RuleStatus> logic, bool requiresConsent) // , Enum grouping
        {
            int ruleValue = (int)Enum.Parse(rule.GetType(), rule.ToString());

            this.RequiresConsent = requiresConsent;
            this.Code = $"#{ruleValue.ToString().PadLeft(4, '0')}";
            this.Name = rule.ToString();
            this.Logic = logic;
            this.Grouping = "Default";
        } 
        
        public void Run(T data, Y variables)
        {
            Status = this.Logic(data, variables);
        }

        public void Skip()
        {
            Status = RuleStatus.Skipped;
        }
    }
}
