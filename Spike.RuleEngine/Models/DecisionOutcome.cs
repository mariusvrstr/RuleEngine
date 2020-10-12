using System.Collections.Generic;

namespace Spike.RuleEngine.Models
{
    public class DecisionOutcome
    {
        public DecisionType Outcome { get; set; }

        public string ReferDescription { get; set; }

        public List<string> FailedRules { get; set; }
    }
}
