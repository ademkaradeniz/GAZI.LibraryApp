using GAZI.LibraryApp.Data.Core;
using GAZI.LibraryApp.Data.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.DatesRepositories
{
    public class DatesRepository : Dates, IDatesRepository
    {
        #region Oracle Komutlar
        GenericRepository _repository;
        OracleConnection _connection;
        OracleCommand _command;

        public DatesRepository()
        {
            _command = new OracleCommand();
            _connection = OracleContext.Connection();
            _repository = new GenericRepository();
        }

        public void Add(Dates dates)
        {
            _repository.Add($"INSERT INTO Dates(PurchaseDate, IssueDate, Status, CreateOrModifyDate) " +
                            $"VALUES('{dates.PurchaseDate.ToString("dd.MM.yyyy")}', '{dates.IssueDate.ToString("dd.MM.yyyy")}', '{1}', '{DateTime.Now.ToString("dd.MM.yyyy")}')");
        }

        public void Delete(Dates dates)
        {
            //_repository.Add($"DELETE FROM Types WHERE ID == {id}");
            _repository.Add($"UPDATE Dates SET Status = {dates.Status}, CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}'  Where ID = {dates.ID}");
        }

        public void Update(Dates dates)
        {
            _repository.Add($"UPDATE Dates SET PurchaseDate = '{dates.PurchaseDate.ToString("dd.MM.yyyy")}', IssueDate = '{dates.IssueDate.ToString("dd.MM.yyyy")}', Status = {dates.Status}" +
                            $", CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}' Where ID = {dates.ID}");
        }

        public List<Dates> GetAll()
        {
            List<Dates> dates = new List<Dates>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Dates WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Dates date = new Dates
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    PurchaseDate = Convert.ToDateTime(rdr["PurchaseDate"]),
                    IssueDate = Convert.ToDateTime(rdr["IssueDate"]),
                    //PastDate = Convert.ToDateTime(rdr["PastDate"]),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                dates.Add(date);
            }
            return dates;
        }

        public Dates FindID(int id)
        {
            Dates dates = new Dates();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Dates WHERE ID = {id} AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Dates list = new Dates
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    PurchaseDate = Convert.ToDateTime(rdr["PurchaseDate"]),
                    IssueDate = Convert.ToDateTime(rdr["IssueDate"]),
                    //PastDate = Convert.ToDateTime(rdr["PastDate"]),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                dates = list;
            }
            return dates;
        }

        public Dates FindDate(Dates date)
        {
            Dates dates = new Dates();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Dates WHERE PurchaseDate = '{date.PurchaseDate.ToString("dd/MM/yyyy")}' AND IssueDate = '{date.IssueDate.ToString("dd/MM/yyyy")}' AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Dates list = new Dates
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    PurchaseDate = Convert.ToDateTime(rdr["PurchaseDate"]),
                    IssueDate = Convert.ToDateTime(rdr["IssueDate"]),
                    //PastDate = Convert.ToDateTime(rdr["PastDate"]),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                dates = list;
            }
            return dates;
        }
        #endregion
    }
}
