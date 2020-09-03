using Spike.RuleEngine.Contracts;
using Spike.RuleEngine.Engine;
using System;

namespace Spike.Engine.RuleEngine
{
    public class Rule<T> 
        where T : IDataModel
    {
        public string Code { get; set; }

        public Func<T, RuleStatus> Logic;

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

        public Rule(Enum rule, Func<T, RuleStatus> logic) // , Enum grouping
        {
            int ruleValue = (int)Enum.Parse(rule.GetType(), rule.ToString());

            this.Code = $"#{ruleValue.ToString().PadLeft(4, '0')}";
            this.Name = rule.ToString();
            this.Logic = logic;
            this.Grouping = "Default";
        } 
        
        public void Run(T data)
        {
            Status = this.Logic(data);
        }
    }
}
