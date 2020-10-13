using Spike.RuleEngine;
using Spike.RuleEngine.Models;
using Spike.RuleEngine.Service;
using System;

namespace Spike.Tests.Stubs
{

    public class CompanyRulesServiceStub : CompanyDetailsRulesEngineServiceBase
    {

        public CompanyRulesServiceStub(CompanyDetails companyDetails, IndividualDetails individualDetails, ClientRiskConfiguration consiguration) 
            : base(new TestProxy(companyDetails, individualDetails, consiguration))
        {
        }       

        public override DecisionOutcome GetCompanyDecision(Guid companyId, string clientCode, bool hasSubjectConsent)
        {
            var cDetails = this.GetCompanyDetails(companyId);
            var clientConf = this.GetClientConfiguration(clientCode);

            // If consent yes
            // If sole prop or single director
            // var iDetails = GetIndividualDetails(companyId);

            var rulesEngine = new CompanyDetailsRulesEngine();
            var results = rulesEngine.ApplyRules(cDetails, hasSubjectConsent);

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
