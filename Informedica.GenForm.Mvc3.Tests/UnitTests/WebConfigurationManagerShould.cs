﻿using System.Configuration;
using System.Web.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Informedica.GenForm.Mvc3.Tests.UnitTests
{
    [TestClass]
    public class WebConfigurationManagerShould
    {
        private const string Testsetting = "testSetting";

        [TestMethod]
        public void BeAbleToOpenASpecificWebConfig()
        {
            try
            {
                WebConfigurationManager.OpenWebConfiguration("/GenForm");
            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            } 
        }

        [TestMethod]
        public void HaveAnConnectionStringForSqLite()
        {
            var man = GetConfigurationManager();
            Assert.IsNotNull(man.ConnectionStrings.ConnectionStrings["Test"]);
        }

        [TestMethod]
        public void BeAbleToAddAndRemoveAConnectionString()
        {
            var man = GetConfigurationManager();
            var testSetting = new ConnectionStringSettings(Testsetting, "this is a test connectionstring");

            man.ConnectionStrings.ConnectionStrings.Add(testSetting);
            man.Save();
            Assert.IsNotNull(man.ConnectionStrings.ConnectionStrings[Testsetting]);

            man = GetConfigurationManager();
            man.ConnectionStrings.ConnectionStrings.Remove(Testsetting);
            man.Save();
            Assert.IsNull(man.ConnectionStrings.ConnectionStrings[Testsetting]);
        }

        private static Configuration GetConfigurationManager()
        {
            var man = WebConfigurationManager.OpenWebConfiguration("/GenForm");
            return man;
        }

    }
}
