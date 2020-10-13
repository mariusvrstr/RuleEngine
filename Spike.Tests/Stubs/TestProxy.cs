using Spike.RuleEngine.Contracts;
using Spike.RuleEngine.Models;
using System;

namespace Spike.Tests.Stubs
{
    public class TestProxy : IProxy
    {
        private CompanyDetails companyDetails { get;  set; }

        private IndividualDetails individualDetails { get; set; }

        private ClientRiskConfiguration configuration { get; set; }


        public TestProxy(CompanyDetails companyDetails, IndividualDetails individualDetails, ClientRiskConfiguration consiguration)
        {

            this.companyDetails = companyDetails;
            this.individualDetails = individualDetails;
            this.configuration = configuration;
        }


        public CompanyDetails GetCompanyDetails(Guid id)
        {
            return companyDetails;
        }

        public IndividualDetails GetIndividualDetails(Guid id)
        {
            return individualDetails;
        }

        public ClientRiskConfiguration GetRiskConfiguration(string clientCode)
        {
            return configuration;
        }
    }
}
