using Spike.RuleEngine;
using Spike.RuleEngine.Contracts;
using Spike.RuleEngine.Models;
using System;

namespace Spike.Tests.Stubs
{

    public class CompanyRulesServiceStub : ICompanyRulesService
    {
        private CompanyDetails companyDetails { get; set; } = null;
        private IndividualDetails individualDetails { get; set; } = null;

        private ClientRiskConfiguration clientRiskConfiguration { get; set; } = null;

        public CompanyRulesServiceStub()
        {
        }

        public void MockCompanyDetails(CompanyDetails companyDetails)
        {
            this.companyDetails = companyDetails;
        }

        public void MockIndividualDetails(IndividualDetails individualDetails)
        {
            this.individualDetails = individualDetails;
        }

        public CompanyDetails GetCompanyDetails(Guid id)
        {
            // Normally this will go to the MD
            return companyDetails;
        }

        public ClientRiskConfiguration GetClientConfiguration(string clientCode)
        {
            // This will come from customer configuration settings
            return clientRiskConfiguration;
        }

        public IndividualDetails GetIndividualDetails(Guid id)
        {
            // This will go to the a consumer credit bureau           

            return individualDetails;
        }

        public DecisionOutcome GetCompanyDecision(Guid companyId, string clientCode)
        {
            var cDetails = GetCompanyDetails(companyId);
            var clientConf = GetClientConfiguration(clientCode);   

            // If consent yes
            // If sole prop or single director
            // var iDetails = GetIndividualDetails(companyId);

            var rulesEngine = new CompanyDetailsRulesEngine();
            var results = rulesEngine.ApplyRules(cDetails);

            // Check knockout
            // Check coverage
            // Check Decline (bracket then collective)
            // Check Refer (bracket then collective)
            // Check Approve (bracket then collective)
            // Check fall through action (else if none of the above)

            return new DecisionOutcome
            {
                FailedRules = results.FailedRules,
                Outcome = DecisionType.Refer,
                ReferDescription = "Ädvance Risk Report"
            };
        }
    }
}
