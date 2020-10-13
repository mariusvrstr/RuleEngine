using Spike.RuleEngine.Contracts;
using Spike.RuleEngine.Models;
using System;

namespace Spike.RuleEngine.Service
{
    public abstract class CompanyDetailsRulesEngineServiceBase : ICompanyRulesService
    {
        public IProxy proxy { get; set; }


        public CompanyDetailsRulesEngineServiceBase() {}

        public CompanyDetailsRulesEngineServiceBase(IProxy proxy)
        {
            this.proxy = proxy;
        }

        public ClientRiskConfiguration GetClientConfiguration(string clientCode)
        {
            return proxy.GetRiskConfiguration(clientCode);
        }

        public abstract DecisionOutcome GetCompanyDecision(Guid companyId, string clientCode, bool hasSubjectConsent);

        public CompanyDetails GetCompanyDetails(Guid id)
        {
            return proxy.GetCompanyDetails(id);
        }

        public IndividualDetails GetIndividualDetails(Guid id)
        {
            return proxy.GetIndividualDetails(id);
        }
    }
}
