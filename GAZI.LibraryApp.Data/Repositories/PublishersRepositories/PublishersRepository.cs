using GAZI.LibraryApp.Data.Core;
using GAZI.LibraryApp.Data.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.PublishersRepositories
{
    public class PublishersRepository : Publishers, IPublishersRepository
    {
        #region Komutlar
        GenericRepository _repository;
        OracleConnection _connection;
        OracleCommand _command;

        public PublishersRepository()
        {
            _command = new OracleCommand();
            _connection = OracleContext.Connection();
            _repository = new GenericRepository();
        }

        public void Add(Publishers publisher)
        {
            _repository.Add($"INSERT INTO Publishers(Name, Status, CreateOrModifyDate) " +
                            $"VALUES('{publisher.Name}', '{1}', '{DateTime.Now.ToString("dd.MM.yyyy")}')");
        }

        public void Delete(Publishers publisher)
        {
            //_repository.Add($"DELETE FROM Publishers WHERE ID == {id}");
            _repository.Add($"UPDATE Publishers SET Status = {publisher.Status}, CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}'  Where ID = {publisher.ID}");
        }

        public void Update(Publishers publisher)
        {
            _repository.Add($"UPDATE Publishers SET Name = '{publisher.Name}', Status = {publisher.Status}" +
                            $", CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}' Where ID = {publisher.ID}");
        }

        public List<Publishers> GetAll()
        {
            List<Publishers> publishers = new List<Publishers>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Publishers WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Publishers publisher = new Publishers
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                publishers.Add(publisher);
            }
            return publishers;
        }

        public Publishers FindID(int id)
        {
            Publishers publisher = new Publishers();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Publishers WHERE ID = {id} AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Publishers list = new Publishers
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                publisher = list;
            }
            return publisher;
        }

        public bool FindName(string name)
        {
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM publishers WHERE REPLACE(LOWER(publishers.name), ' ', '') LIKE REPLACE(LOWER('%{name}%'), ' ', '') AND Status=1";
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
