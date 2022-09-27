using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectingToLocalSQLServer
{
    class DataFactory
    {
        private static readonly string SqlConnectionString;
        //private static readonly string AsyncBrokerConnectionString;

        static DataFactory()
        {
            SqlConnectionString = ConfigurationManager.ConnectionStrings["ChangeTrackingDB"].ConnectionString;
            //AsyncBrokerConnectionString = ConfigurationManager.ConnectionStrings["AsyncBrokerConnectionString"].ConnectionString;
        }


        public static SqlConnection CreateSqlConnection(bool open = true)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            if (open)
                connection.Open();
            return connection;
        }

        public static DataTable GetDataTable(SqlCommand cmd)
        {
            SqlDataAdapter adapt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapt.Fill(dt);
            adapt.Dispose();
            return dt;
        }
    }
}
