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
        CheckYoungCompanyInBusiness = 2
    }

           
    public class CompanyDetailsRulesEngine : RuleEngine<CompanyDetails, CompanyVariables>
    {
        // Example of how variables could be set
        public override void SetVariables(CompanyDetails details, ref CompanyVariables variables)
        {
            if (details.RegisteredDate != null)
            {
                var days = (DateTime.Now - details.RegisteredDate).TotalDays;

                variables.IsYoungBusiness = (days < 365);
            }          
        }

        #region Pool of Available Rules

        public CompanyDetailsRulesEngine(List<string> whitelistRules = null) : base(whitelistRules) { }
   
        private Func<CompanyDetails, CompanyVariables, RuleStatus> CheckBusinessIsInHealthyState = delegate(CompanyDetails data, CompanyVariables variables)
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

        private Func<CompanyDetails, CompanyVariables, RuleStatus> CheckYoungCompanyInBusiness = delegate (CompanyDetails data, CompanyVariables variables)
        {
            if (variables?.IsYoungBusiness == null || data?.Status == null)
            {
                return RuleStatus.Skipped;
            }

            if (data.Status == BusinessStatus.InBusiness && variables.IsYoungBusiness.Value)
            {
                return RuleStatus.Failed;
            }

            return RuleStatus.Succeeded;
        };


        #endregion


        public override void InitializeRules()
        { 
            AddRule(CompanyDetailsRule.CheckBusinessIsInHealthyState, CheckBusinessIsInHealthyState);
            AddRule(CompanyDetailsRule.CheckYoungCompanyInBusiness, CheckYoungCompanyInBusiness, true);
        }

    }
}
