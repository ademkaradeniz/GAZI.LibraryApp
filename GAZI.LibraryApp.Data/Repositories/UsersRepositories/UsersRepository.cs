using GAZI.LibraryApp.Data.Core;
using GAZI.LibraryApp.Data.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.UsersRepositories
{
    public class UsersRepository : Users, IUsersRepository
    {
        #region Oracle Komutlar
        GenericRepository _repository;
        OracleConnection _connection;
        OracleCommand _command;

        public UsersRepository()
        {
            _command = new OracleCommand();
            _connection = OracleContext.Connection();
            _repository = new GenericRepository();
        }

        public void Add(Users users)
        {
            _repository.Add($"INSERT INTO Users(RoleID, Name, SurName, UserName, Email, Password, AuthCode, Telephone, Status, CreateOrModifyDate) " +
                            $"VALUES({users.RoleID},'{users.Name}','{users.SurName}','{users.UserName}','{users.Email}','{users.Password}','{users.AuthCode}','{users.Telephone}', '{1}', '{DateTime.Now.ToString("dd.MM.yyyy")}')");
        }

        public void Delete(Users users)
        {
            //_repository.Add($"DELETE FROM Users WHERE ID == {id}");
            _repository.Add($"UPDATE Users SET Status = {users.Status}, CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}'  Where ID = {users.ID}");
        }

        public void Update(Users users)
        {
            _repository.Add($"UPDATE Users SET RoleID={users.RoleID}, Name = '{users.Name}', SurName = '{users.SurName}', UserName = '{users.UserName}', " +
                $"Email = '{users.Email}', Password = '{users.Password}', AuthCode = '{users.AuthCode}', Telephone = '{users.Telephone}', Status = {users.Status}" +
                            $", CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}' Where ID = {users.ID}");
        }

        public List<Users> GetAll()
        {
            List<Users> users = new List<Users>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Users WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Users user = new Users
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    RoleID = Convert.ToInt32(rdr["RoleID"]),
                    Name = rdr["Name"].ToString(),
                    SurName = rdr["SurName"].ToString(),
                    UserName = rdr["UserName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Password = rdr["Password"].ToString(),
                    AuthCode = rdr["AuthCode"].ToString(),
                    Telephone = rdr["TELEPHONE"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                users.Add(user);
            }
            return users;
        }

        public List<ViewUsers> GetAllView()
        {
            List<ViewUsers> users = new List<ViewUsers>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM ViewUsers WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                ViewUsers user = new ViewUsers
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    RoleID = Convert.ToInt32(rdr["RoleID"]),
                    RoleName = rdr["RoleName"].ToString(),
                    Name = rdr["Name"].ToString(),
                    SurName = rdr["SurName"].ToString(),
                    UserName = rdr["UserName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Password = rdr["Password"].ToString(),
                    AuthCode = rdr["AuthCode"].ToString(),
                    Telephone = rdr["Telephone"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                users.Add(user);
            }
            return users;
        }

        public Users FindID(int? id)
        {
            Users users = new Users();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Users WHERE ID = {id} AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Users user = new Users
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    RoleID = Convert.ToInt32(rdr["RoleID"]),
                    Name = rdr["Name"].ToString(),
                    SurName = rdr["SurName"].ToString(),
                    UserName = rdr["UserName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Password = rdr["Password"].ToString(),
                    AuthCode = rdr["AuthCode"].ToString(),
                    Telephone = rdr["Telephone"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                users = user;
            }
            return users;
        }

        public Users FindUserNameAndPassword(string username, string password)
        {
            Users users = new Users();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Users WHERE UserName = '{username}' AND Password = '{password}' AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Users user = new Users
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    RoleID = Convert.ToInt32(rdr["RoleID"]),
                    Name = rdr["Name"].ToString(),
                    SurName = rdr["SurName"].ToString(),
                    UserName = rdr["UserName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Password = rdr["Password"].ToString(),
                    AuthCode = rdr["AuthCode"].ToString(),
                    Telephone = rdr["Telephone"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                users = user;
            }
            return users;
        }

        public bool FindUserNameOrEmail(string username,string email)
        {
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM users WHERE REPLACE(LOWER(users.username), ' ', '') LIKE REPLACE(LOWER('%{username}%'), ' ', '') OR REPLACE(LOWER(users.email), ' ', '') LIKE REPLACE(LOWER('%{email}%'), ' ', '') AND Status=1";
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

        public Users FindEmail(string email)
        {
            Users users = new Users();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Users WHERE Email = '{email}' AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Users user = new Users
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    RoleID = Convert.ToInt32(rdr["RoleID"]),
                    Name = rdr["Name"].ToString(),
                    SurName = rdr["SurName"].ToString(),
                    UserName = rdr["UserName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Password = rdr["Password"].ToString(),
                    AuthCode = rdr["AuthCode"].ToString(),
                    Telephone = rdr["Telephone"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                users = user;
            }
            return users;
        }
        #endregion
    }
}
