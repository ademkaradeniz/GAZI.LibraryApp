using GAZI.LibraryApp.Data.Core;
using GAZI.LibraryApp.Data.Entities;
using GAZI.LibraryApp.Data.Repositories.UsersRepositories;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.RolesRepositories
{
    public class RolesRepository : Roles, IRolesRepository
    {
        #region Oracle Komutlar
        GenericRepository _repository;
        OracleConnection _connection;
        OracleCommand _command;

        public RolesRepository()
        {
            _command = new OracleCommand();
            _connection = OracleContext.Connection();
            _repository = new GenericRepository();
        }

        public void Add(Roles roles)
        {
            _repository.Add($"INSERT INTO Roles(Name, Status, CreateOrModifyDate) " +
                            $"VALUES('{roles.Name}', '{1}', '{DateTime.Now.ToString("dd.MM.yyyy")}')");
        }

        public void Delete(Roles roles)
        {
            //_repository.Add($"DELETE FROM Roles WHERE ID == {id}");
            _repository.Add($"UPDATE Roles SET Status = {roles.Status}, CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}'  Where ID = {roles.ID}");
        }

        public void Update(Roles roles)
        {
            _repository.Add($"UPDATE Roles SET Name = '{roles.Name}', Status = {roles.Status}" +
                            $", CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}' Where ID = {roles.ID}");
        }

        public List<Roles> GetAll()
        {
            List<Roles> roless = new List<Roles>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Roles WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Roles roles = new Roles
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                roless.Add(roles);
            }
            return roless;
        }

        public Roles FindID(int? id)
        {
            Roles roles = new Roles();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Roles WHERE ID = {id} AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Roles list = new Roles
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                roles = list;
            }
            return roles;
        }

        public bool FindName(string name)
        {
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM roles WHERE REPLACE(LOWER(roles.name), ' ', '') LIKE REPLACE(LOWER('%{name}%'), ' ', '') AND Status=1";
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

        public int GetID(string name)
        {
            int RoleID = 0;
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM roles WHERE REPLACE(LOWER(roles.name), ' ', '') LIKE REPLACE(LOWER('%{name}%'), ' ', '') AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {

                RoleID = Convert.ToInt32(rdr["ID"]);
            }
            return RoleID;
        }
        #endregion
    }
}
