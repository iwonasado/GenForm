using System;
using System.Linq;

namespace Informedica.GenForm.Settings
{
    public class GenFormEnvironment
    {
        private const int DatabaseConnectionStringIndex = 0;
        private const int LogPathIndex = 1;
        private const int ExporthPathIndex = 2;

        private readonly Environment _environment;

        public GenFormEnvironment(Environment environment)
        {
            if (environment.Settings.Count() < 3) throw new Exception("Not enough settings!");
            _environment = environment;
        }

        public string Name { get { return _environment.Name; } }

        public string GenFormDatabaseConnectionString
        {
            get { return GetConnectionString(); }
            set { _environment.Settings.ElementAt(DatabaseConnectionStringIndex).ConnectionString = value; }
        }

        private string GetConnectionString()
        {
            return _environment.Settings.Count() <= DatabaseConnectionStringIndex ? 
                string.Empty : 
                _environment.Settings.ElementAt(DatabaseConnectionStringIndex).ConnectionString;
        }

        public string LogPath
        {
            get { return _environment.Settings.ElementAt(LogPathIndex).ConnectionString; }
            set { _environment.Settings.ElementAt(LogPathIndex).ConnectionString = value; }
        }

        public string ExportPath
        {
            get { return _environment.Settings.ElementAt(ExporthPathIndex).ConnectionString; }
            set { _environment.Settings.ElementAt(ExporthPathIndex).ConnectionString = value; }
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(GenFormDatabaseConnectionString);
        }
    }
}