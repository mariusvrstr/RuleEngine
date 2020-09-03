﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spike.RuleEngine;
using Spike.Tests.Builders;
using System.Collections.Generic;

namespace Spike.Tests
{
    [TestClass]
    public class TestCompanyDetailsRuleEngine
    {
        public List<string> WhitelistRule = new List<string> {"#0001"};


        [TestMethod]
        public void TestMustHaveRules()
        {
            var companyToTest = new CompanyDetailsBuilder()
                .Tesla()
                .HealthyBusiness()
                .Build();

            var rulesEngine = new CompanyDetailsRulesEngine();
            var results = rulesEngine.ApplyRules(companyToTest);

            Assert.IsTrue(results.TotalApplicableRules > 0);
        }


        [TestMethod]
        public void TestBusinessSuccess()
        {
            var companyToTest = new CompanyDetailsBuilder()
                .Tesla()
                .HealthyBusiness()
                .Build();

            var rulesEngine = new CompanyDetailsRulesEngine();
            var results = rulesEngine.ApplyRules(companyToTest);

            Assert.IsTrue(results.PercentageSucceeded > 0);
        }

        [TestMethod]
        public void TestBusinessSuccessWithWhitelistRules()
        {
            var companyToTest = new CompanyDetailsBuilder()
                .Tesla()
                .HealthyBusiness()
                .Build();

            var rulesEngine = new CompanyDetailsRulesEngine(WhitelistRule);
            var results = rulesEngine.ApplyRules(companyToTest);

            Assert.IsTrue(results.TotalIgnoredRules > 0);
        }


    }
}