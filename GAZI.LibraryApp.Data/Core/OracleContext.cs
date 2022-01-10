using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Core
{
    public static class OracleContext
    {
        public static OracleConnection Connection()
        {
            var connectionString = "User Id = GAZI;Password = 123;Data Source = localhost:1521/XEPDB1; Pooling=False;";
            OracleConnection connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
