using Spike.RuleEngine.Models;
using System;

namespace Spike.Tests.Builders
{
    public class CompanyDetailsBuilder : CompanyDetails
    {
        public CompanyDetailsBuilder(Guid? id = null)
        {
            this.Id = id ?? Guid.NewGuid();
        }

        public CompanyDetailsBuilder Tesla()
        {
            this.EntityName = "Tesla";
            this.CEO = "Elon Musk";
            this.RegisteredDate = new DateTime(2015,1,1);

            return this;
        }


        public CompanyDetailsBuilder HealthyBusiness()
        {
            this.Status = BusinessStatus.InBusiness;

            return this;
        }

        public CompanyDetailsBuilder OutOfBusiness()
        {
            this.Status = BusinessStatus.Closed;

            return this;
        }


        public CompanyDetails Build()
        {
            return this;
        }

    }
}
