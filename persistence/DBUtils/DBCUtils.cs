using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace LabMPP.JdbcUtils
{
    class DBCUtils
    {
        public static SqlConnection GetConnection()
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["festivals"];
            // String connectionString = "Data Source=MOHIWORKSTATION;Initial Catalog=festival;Integrated Security=true;";
            if (connectionString != null)
                return new SqlConnection(connectionString.ToString());

            return null;
        }
    }
}
