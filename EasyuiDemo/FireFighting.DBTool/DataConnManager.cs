using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FireFighting.DBTool
{
    public static class DataConnManager
    {
        private static string connectionString;
        private static string connectionStringBusiness;

        public static MySqlCommand GetDataBase()
        {
            using (MySqlConnection MySqlConnection = new MySqlConnection(ConnectionString()))
            {
                return MySqlConnection.CreateCommand();
            }
        }

        public static string ConnectionString()
        {
            if (connectionString == null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["MySqlConnString"].ConnectionString;
            }
            return connectionString.ToString();
        }
        public static string BusinessConnectionString()
        {
            if (connectionStringBusiness == null)
            {
                connectionStringBusiness = ConfigurationManager.ConnectionStrings["MySqlConnString"].ConnectionString;
            }
            return connectionStringBusiness.ToString();
        }
    }
}
