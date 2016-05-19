using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace IS412_NET_Implementation_Final_Project
{
    internal class Utility
    {
        // Get the connection string from App config file
        internal static string GetConnectionString()
        {
            string returnValue = null;

            // Look for the name in the connectionStrings section
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["IS412_NET_Implementation_Final_Project.Properties.Settings.connString"];

            // If found, return the connection string
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
