
using Spike.RuleEngine.Models;
using System;

namespace Spike.RuleEngine.Contracts
{
    public interface ICompanyRulesService
    {
        CompanyDetails GetCompanyDetails(Guid id);

        IndividualDetails GetIndividualDetails(Guid id);

        ClientRiskConfiguration GetClientConfiguration(string clientCode);

        DecisionOutcome GetCompanyDecision(Guid companyId, string clientCode);
    }
}
