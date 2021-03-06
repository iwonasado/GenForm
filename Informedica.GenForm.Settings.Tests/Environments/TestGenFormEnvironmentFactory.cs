using Informedica.GenForm.Settings.Environments;
using Informedica.SecureSettings.Cryptographers;
using TypeMock.ArrangeActAssert;
using Environment = Informedica.GenForm.Settings.Environments.Environment;

namespace Informedica.GenForm.Settings.Tests.Environments
{
    static internal class TestGenFormEnvironmentFactory
    {
        public static GenFormEnvironment CreateTestGenFormEnvironment()
        {
            var source = new TestSource();
            
            var keyMan = Isolate.Fake.Instance<SecretKeyManager>();
            Isolate.WhenCalled(() => keyMan.GetKey()).WillReturn("secret");
            Isolate.WhenCalled(() => keyMan.SetKey("secret")).IgnoreCall();


            var envSets = new EnvironmentSettingsCollection(source);
            envSets.AddSetting("MyMachine", "Test", "Database", "Provider", "Some connection string");
            envSets.AddSetting("MyMachine", "Test", "LogPath", "File", string.Empty);
            envSets.AddSetting("MyMachine", "Test","ExportPath", "File", string.Empty);

            var env = new Environment("MyMachine", "Test", envSets);
            return new GenFormEnvironment(env);
        }
    }
}