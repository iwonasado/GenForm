﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenForm.Database
{
    public static class DatabaseConnection
    {
        public enum DatabaseName 
        {
            FORMULARIUM2010,
            GENPRES, 
            GenForm
        }

        public static string GetConnectionString(DatabaseName database) 
        {
            string connection = string.Empty;

            switch (database){
                case DatabaseName.FORMULARIUM2010:
                    try
                    {
                        connection = System.Configuration.ConfigurationManager.ConnectionStrings[GetConnectionName(database)].ConnectionString;
                    }
                    catch (Exception e)
                    {
                        // Temporary solution because Linqpad cannot locate app.config
                        connection = "Data Source=HAL-2008;Initial Catalog=Formularium2010;Integrated Security=True";
                    }
                    break;
                case DatabaseName.GenForm:
                    try
                    {
                        connection = System.Configuration.ConfigurationManager.ConnectionStrings[GetConnectionName(database)].ConnectionString;
                    }
                    catch (Exception e)
                    {
                        // Temporary solution because Linqpad cannot locate app.config
                        connection = "Data Source=HAL-2008;Initial Catalog=GenForm;Integrated Security=True";
                    }
                    break;
                default:
                    throw new Exception("Database not found");
            }

            return connection;
            
        }

        private static string GetConnectionName(DatabaseName database) 
        {
            return (GetComputerName() + "_" + Enum.GetName(typeof(DatabaseName), database));
        }


        public static string GetComputerName()
        {
            return System.Environment.MachineName;
        }

    }
}
