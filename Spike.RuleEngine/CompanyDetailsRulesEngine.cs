using Spike.Engine.RuleEngine;
using Spike.RuleEngine.Engine;
using Spike.RuleEngine.Models;
using System;
using System.Collections.Generic;

namespace Spike.RuleEngine
{

    public enum CompanyDetailsRule
    {
        Undefined = 0,
        CheckBusinessIsInHealthyState = 1,
        CheckCompanyInBusinessFor12Months = 2
    }
           
    public class CompanyDetailsRulesEngine : RuleEngine<CompanyDetails>
    {
        public CompanyDetailsRulesEngine(List<string> whitelistRules = null) : base(whitelistRules) { }
   
                private Func<CompanyDetails, RuleStatus> CheckBusinessIsInHealthyState = delegate(CompanyDetails data)
        {
            if (data == null)
            {
                return RuleStatus.Skipped;
            }

            // Check conditions 
            if (data.Status != BusinessStatus.InBusiness)
            {
                return RuleStatus.Failed;
            }

            return RuleStatus.Succeeded;
        };

        private Func<CompanyDetails, RuleStatus> CheckCompanyInBusinessFor12Months = delegate (CompanyDetails data)
        {
            if (data?.RegisteredDate == null)
            {
                return RuleStatus.Skipped;
            }

            if ((DateTime.Now.Year - data?.RegisteredDate.Year) < 12)
            {
                return RuleStatus.Failed;
            }

            return RuleStatus.Succeeded;
        };        


        public override void InitializeRules()
        { 
            AddRule(CompanyDetailsRule.CheckBusinessIsInHealthyState, CheckBusinessIsInHealthyState);
            AddRule(CompanyDetailsRule.CheckCompanyInBusinessFor12Months, CheckCompanyInBusinessFor12Months);
        }
    }
}
