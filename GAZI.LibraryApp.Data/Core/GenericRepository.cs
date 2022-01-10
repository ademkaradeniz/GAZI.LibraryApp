using GAZI.LibraryApp.Data.Core;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Core
{
    public class GenericRepository : IGenericRepository
    {
        OracleConnection _connection;
        OracleCommand _command;
        public GenericRepository()
        {
            _connection = OracleContext.Connection();
            _command = new OracleCommand();
        }

        public void Add(string query)
        {
            _command.Connection = _connection;
            _command.CommandText = query;
            _command.ExecuteNonQuery();
        }
        public void Delete(string query)
        {
            _command.Connection = _connection;
            _command.CommandText = query;
            _command.ExecuteNonQuery();
        }
        public void Update(string query)
        {
            _command.Connection = _connection;
            _command.CommandText = query;
            _command.ExecuteNonQuery();
        }
    }
}
