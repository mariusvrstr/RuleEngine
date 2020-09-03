
using System.Collections.Generic;

namespace Spike.RuleEngine.Engine
{
    public class Result
    {
        public int TotalApplicableRules { get; set; }

        public int PercentageSucceeded { get; set; }

        public int PercentageFailed{ get; set; }

        public int TotalIgnoredRules { get; set; }

        public int PercentageSkipped { get; set; }

        public List<string> FailedRules { get; set; }

    }
}
