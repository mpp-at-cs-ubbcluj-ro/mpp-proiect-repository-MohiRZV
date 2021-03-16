using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Lab2MPP.JdbcUtils
{
    class DBCUtils
    {
        public static SqlConnection GetConnection()
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["festivals"];
            if (connectionString != null)
                return new SqlConnection(connectionString.ToString());

            return null;
        }
    }
}
