using Spike.RuleEngine.Contracts;
using System;

namespace Spike.RuleEngine.Models
{
    public enum BusinessStatus
    {
        Undefined = 0,
        InBusiness = 1,
        Closed = 2,
        Liquidation = 3
    }

    public class CompanyDetails : IDataModel
    {
        public Guid Id { get; set; }

        public string EntityName { get; set; }

        public string CEO { get; set; }

        public BusinessStatus Status { get; set; }

        public DateTime RegisteredDate { get; set; }

    }
}
