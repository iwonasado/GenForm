using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Informedica.SecureSettings;
using Informedica.SecureSettings.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypeMock;
using TypeMock.ArrangeActAssert;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    [TestClass]
    public class EnvironmentsShould
    {

        [Isolated]
        [TestMethod]
        public void NotAcceptTheSameSettingTwice()
        {
            var envs = GetEnvironments();
            var setting = GetFakeEnvironmentSetting();
            Isolate.WhenCalled(() => setting.IsIdentical(setting)).WillReturn(true);

            envs.AddSetting(setting);

            try
            {
                envs.AddSetting(setting);
                Assert.Fail("should not accept the same setting twice");
            }
            catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(AssertFailedException), e.ToString());
            }

        }

        [Isolated]
        [TestMethod]
        public void WhenThereAreNoEnvironmentsReturnZeroEnvironmentCount()
        {
            var envs = GetEnvironmentSettingsWithCountZero();

            Assert.AreEqual(0, envs.Count());
        }

        private static IEnumerable<EnvironmentSetting> GetEnvironmentSettingsWithCountZero()
        {
            var setman = Isolate.Fake.Instance<SettingsManager>();
            Isolate.WhenCalled(() => setman.GetConnectionStrings()).DoInstead(ReturnEmptyConnectionStringList);

            return new EnvironmentSettings(setman, "Test", "Test");
        }

        private static IEnumerable<ConnectionStringSettings> ReturnEmptyConnectionStringList(MethodCallContext arg)
        {
            return new List<ConnectionStringSettings>();
        }

        [Isolated]
        [TestMethod]
        public void BeAbleToAddAnTestEnvironment()
        {
            var env = GetFakeEnvironmentSetting();
            var envs = GetEnvironments();

            try
            {
                envs.AddSetting(env);

            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        private static EnvironmentSetting GetFakeEnvironmentSetting()
        {
            var env = Isolate.Fake.Instance<EnvironmentSetting>();
            Isolate.WhenCalled(() => env.Id).WillReturn(1);
            Isolate.WhenCalled(() => env.MachineName).WillReturn("test");
            Isolate.WhenCalled(() => env.Name).WillReturn("test");
            Isolate.WhenCalled(() => env.Provider).WillReturn("test");
            Isolate.WhenCalled(() => env.ConnectionString).WillReturn("test");

            return env;
        }

        [Isolated]
        [TestMethod]
        public void BeAbleToRemoveAnTestEnvironment()
        {
            var envs = GetEnvironments();
            var env = GetFakeEnvironmentSetting();

            envs.AddSetting(env);

            try
            {
                envs.RemoveEnvironment(env);

            }
            catch (System.Exception e)
            {
                Assert.Fail(e.ToString());
            }
        }

        [Isolated]
        [TestMethod]
        public void HaveTestEnvironmentWhenTestEnvironmentIsAdded()
        {
            const string machine = "test";
            const string environment = "test";

            var source = new TestSettingSource();
            var envs = new EnvironmentSettings(new SettingsManager(new SecureSettingsManager(source)), machine, environment);
            var env = new EnvironmentSetting(1, machine, environment, "test", "test");
            envs.AddSetting(env);

            Assert.IsTrue(envs.Any(e => e.Name == env.Name));
        }

        [Isolated]
        [TestMethod]
        public void BeAbleToRemoveTestEnvironmentAfterTestEnvironmentIsAdded()
        {
            var source = new TestSettingSource();
            var envs = new EnvironmentSettings(new SettingsManager(new SecureSettingsManager(source)), "test", "test");
            var env = new EnvironmentSetting(1, "test", "test", "test", "test");
            envs.AddSetting(env);
            envs.RemoveEnvironment(env);

            Assert.IsFalse(envs.Any(e => e.Name == env.Name));
        }

        private static EnvironmentSettings GetEnvironments()
        {
            var setman = GetFakeSettingsManager();

            return new EnvironmentSettings(setman, "Test", "Test");
        }

        private static SettingsManager GetFakeSettingsManager()
        {
            var setman = Isolate.Fake.Instance<SettingsManager>();
            Isolate.WhenCalled(() => setman.AddConnectionString("test.test.test", "test")).IgnoreCall();
            Isolate.WhenCalled(() => setman.RemoveConnectionString("test.test.test")).IgnoreCall();

            return setman;
        }
    }
}