using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace VideoKlubApp
{
    public static class DBHelper
    {
        private static string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=VideotekaDB;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}