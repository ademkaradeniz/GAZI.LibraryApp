using GAZI.LibraryApp.Data.Core;
using GAZI.LibraryApp.Data.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.BorrowBooksRepositories
{
    public class BorrowBooksRepository : BorrowBooks, IBorrowBooksRepository
    {
        #region Oracle Komutlar
        GenericRepository _repository;
        OracleConnection _connection;
        OracleCommand _command;

        public BorrowBooksRepository()
        {
            _command = new OracleCommand();
            _connection = OracleContext.Connection();
            _repository = new GenericRepository();
        }

        public void Add(BorrowBooks borrowBooks)
        {
            _repository.Add($"INSERT INTO BorrowBooks(UserID, BookID, DateID, Status, CreateOrModifyDate) " +
                            $"VALUES('{borrowBooks.UserID}', '{borrowBooks.BookID}', '{borrowBooks.DateID}', '{1}', " +
                            $"'{DateTime.Now.ToString("dd.MM.yyyy")}')");
        }

        public void Delete(BorrowBooks borrowBooks)
        {
            //_repository.Add($"DELETE FROM Types WHERE ID == {id}");
            _repository.Add($"UPDATE BorrowBooks SET Status = {borrowBooks.Status}, CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}'  Where ID = {borrowBooks.ID}");
        }

        public void Update(BorrowBooks borrowBooks)
        {
            _repository.Add($"UPDATE BorrowBooks SET UserID = '{borrowBooks.UserID}', BookID = '{borrowBooks.BookID}', DateID = '{borrowBooks.DateID}', Status = {borrowBooks.Status}" +
                            $", CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}' Where ID = {borrowBooks.ID}");
        }

        public List<BorrowBooks> GetAll()
        {
            List<BorrowBooks> borrowBooks = new List<BorrowBooks>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM BorrowBooks WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                BorrowBooks borrowBook = new BorrowBooks
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    UserID = Convert.ToInt32(rdr["UserID"]),
                    BookID = Convert.ToInt32(rdr["BookID"]),
                    DateID = Convert.ToInt32(rdr["DateID"]),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                borrowBooks.Add(borrowBook);
            }
            return borrowBooks;
        }

        public List<ViewBorrowBooks> GetAllViewBorrowBooks()
        {
            List<ViewBorrowBooks> borrowBooks = new List<ViewBorrowBooks>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM ViewBorrowBooks WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                ViewBorrowBooks borrowBook = new ViewBorrowBooks
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    UserID = Convert.ToInt32(rdr["UserID"]),
                    RoleID = Convert.ToInt32(rdr["RoleID"]),
                    RoleName = rdr["RoleName"].ToString(),
                    Name = rdr["Name"].ToString(),
                    SurName = rdr["SurName"].ToString(),
                    UserName = rdr["UserName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Telephone = rdr["Telephone"].ToString(),
                    BookID = Convert.ToInt32(rdr["BookID"]),
                    BookName = rdr["BookName"].ToString(),
                    AuthorID = Convert.ToInt32(rdr["AuthorID"]),
                    AuthorName = rdr["AuthorName"].ToString(),
                    PublisherID = Convert.ToInt32(rdr["PublisherID"]),
                    PublisherName = rdr["PublisherName"].ToString(),
                    TypeID = Convert.ToInt32(rdr["TypeID"]),
                    TypeName = rdr["TypeName"].ToString(),
                    YearOfPublic = rdr["YearOfPublic"].ToString(),
                    NumberOfPage = Convert.ToInt32(rdr["NumberOfPage"]),
                    Image = rdr["Image"].ToString(),
                    DateID = Convert.ToInt32(rdr["DateID"]),
                    PurchaseDate = Convert.ToDateTime(rdr["PurchaseDate"]),
                    IssueDate = Convert.ToDateTime(rdr["IssueDate"]),
                    //PastDate = Convert.ToDateTime(rdr["PastDate"]),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                borrowBooks.Add(borrowBook);
            }
            return borrowBooks;
        }

        public List<ViewBorrowBooks> GetAllUserViewBorrowBooks(int Id)
        {
            List<ViewBorrowBooks> borrowBooks = new List<ViewBorrowBooks>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM ViewBorrowBooks WHERE UserID={Id} AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                ViewBorrowBooks borrowBook = new ViewBorrowBooks
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    UserID = Convert.ToInt32(rdr["UserID"]),
                    RoleID = Convert.ToInt32(rdr["RoleID"]),
                    RoleName = rdr["RoleName"].ToString(),
                    Name = rdr["Name"].ToString(),
                    SurName = rdr["SurName"].ToString(),
                    UserName = rdr["UserName"].ToString(),
                    Email = rdr["Email"].ToString(),
                    Telephone = rdr["Telephone"].ToString(),
                    BookID = Convert.ToInt32(rdr["BookID"]),
                    BookName = rdr["BookName"].ToString(),
                    AuthorID = Convert.ToInt32(rdr["AuthorID"]),
                    AuthorName = rdr["AuthorName"].ToString(),
                    PublisherID = Convert.ToInt32(rdr["PublisherID"]),
                    PublisherName = rdr["PublisherName"].ToString(),
                    TypeID = Convert.ToInt32(rdr["TypeID"]),
                    TypeName = rdr["TypeName"].ToString(),
                    YearOfPublic = rdr["YearOfPublic"].ToString(),
                    NumberOfPage = Convert.ToInt32(rdr["NumberOfPage"]),
                    Image = rdr["Image"].ToString(),
                    DateID = Convert.ToInt32(rdr["DateID"]),
                    PurchaseDate = Convert.ToDateTime(rdr["PurchaseDate"]),
                    IssueDate = Convert.ToDateTime(rdr["IssueDate"]),
                    //PastDate = Convert.ToDateTime(rdr["PastDate"]),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                borrowBooks.Add(borrowBook);
            }
            return borrowBooks;
        }

        public BorrowBooks FindID(int id)
        {
            BorrowBooks borrowBooks = new BorrowBooks();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM BorrowBooks WHERE ID = {id} AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                BorrowBooks borrowBook = new BorrowBooks
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    UserID = Convert.ToInt32(rdr["UserID"]),
                    BookID = Convert.ToInt32(rdr["BookID"]),
                    DateID = Convert.ToInt32(rdr["DateID"]),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                borrowBooks = borrowBook;
            }
            return borrowBooks;
        }

        #endregion
    }
}
