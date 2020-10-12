
using System;
using System.Collections.Generic;

namespace Spike.RuleEngine.Models
{
    public class ClientRiskConfiguration
    {
        public Guid Id { get; set; }

        public string ClientName { get; set; }

        public List<string> ApplicableRules { get; set; }

        public List<string> KnockoutRules { get; set; }

        public List<string> Variables { get; set; }

        public string CoverageCheck { get; set; }

        public List<string> BracketChecks { get; set; }

        public List<string> CollectionChecks { get; set; }

        public string DefaultFallThroughAction { get; set; }

    }
}
