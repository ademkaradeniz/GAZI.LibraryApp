using GAZI.LibraryApp.Data.Core;
using GAZI.LibraryApp.Data.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.TypesRepositories
{
    public class TypesRepository : Types, ITypesRepository
    {
        #region Oracle Komutlar
        GenericRepository _repository;
        OracleConnection _connection;
        OracleCommand _command;

        public TypesRepository()
        {
            _command = new OracleCommand();
            _connection = OracleContext.Connection();
            _repository = new GenericRepository();
        }

        public void Add(Types types)
        {
            _repository.Add($"INSERT INTO Types(Name, Status, CreateOrModifyDate) " +
                            $"VALUES('{types.Name}', '{1}', '{DateTime.Now.ToString("dd.MM.yyyy")}')");
        }

        public void Delete(Types types)
        {
            //_repository.Add($"DELETE FROM Types WHERE ID == {id}");
            _repository.Add($"UPDATE Types SET Status = {types.Status}, CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}'  Where ID = {types.ID}");
        }

        public void Update(Types types)
        {
            _repository.Add($"UPDATE Types SET Name = '{types.Name}', Status = {types.Status}" +
                            $", CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}' Where ID = {types.ID}");
        }

        public List<Types> GetAll()
        {
            List<Types> typess = new List<Types>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Types WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Types types = new Types
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                typess.Add(types);
            }
            return typess;
        }

        public Types FindID(int id)
        {
            Types types = new Types();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Types WHERE ID = {id} AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Types list = new Types
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                types = list;
            }
            return types;
        }

        public bool FindName(string name)
        {
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM types WHERE REPLACE(LOWER(types.name), ' ', '') LIKE REPLACE(LOWER('%{name}%'), ' ', '') AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            int rowCount = 0;
            while (rdr.Read())
            {
                rowCount++;
            }
            if (rowCount != 0)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
