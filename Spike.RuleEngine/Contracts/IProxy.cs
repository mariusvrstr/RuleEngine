using Spike.RuleEngine.Models;
using System;

namespace Spike.RuleEngine.Contracts
{
    public interface IProxy
    {
        ClientRiskConfiguration GetRiskConfiguration(string clientCode);

        CompanyDetails GetCompanyDetails(Guid id);

        IndividualDetails GetIndividualDetails(Guid id);

    }
}
