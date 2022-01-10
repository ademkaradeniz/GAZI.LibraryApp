using GAZI.LibraryApp.Data.Core;
using GAZI.LibraryApp.Data.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.AuthorsRepositories
{
    public class AuthorsRepository : Authors, IAuthorsRepository
    {
        #region Komutlar
        GenericRepository _repository;
        OracleConnection _connection;
        OracleCommand _command;

        public AuthorsRepository()
        {
            _command = new OracleCommand();
            _connection = OracleContext.Connection();
            _repository = new GenericRepository();
        }

        public void Add(Authors author)
        {
            _repository.Add($"INSERT INTO Authors(Name, Status, CreateOrModifyDate) " +
                            $"VALUES('{author.Name}', '{1}', '{DateTime.Now.ToString("dd.MM.yyyy")}')");
        }

        public void Delete(Authors author)
        {
            //_repository.Add($"DELETE FROM Authors WHERE ID == {id}");
            _repository.Add($"UPDATE Authors SET Status = {author.Status}, CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}'  Where ID = {author.ID}");
        }

        public void Update(Authors author)
        {
            _repository.Add($"UPDATE Authors SET Name = '{author.Name}', Status = {author.Status}" +
                            $", CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}' Where ID = {author.ID}");
        }

        public List<Authors> GetAll()
        {
            List<Authors> authors = new List<Authors>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Authors WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Authors author = new Authors
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                authors.Add(author);
            }
            return authors;
        }

        public Authors FindID(int id)
        {
            Authors author = new Authors();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Authors WHERE ID = {id} AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Authors yazar = new Authors
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                author = yazar;
            }
            return author;
        }

        public bool FindName(string name)
        {
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM authors WHERE REPLACE(LOWER(authors.name), ' ', '') LIKE REPLACE(LOWER('%{name}%'), ' ', '') AND Status=1";
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
