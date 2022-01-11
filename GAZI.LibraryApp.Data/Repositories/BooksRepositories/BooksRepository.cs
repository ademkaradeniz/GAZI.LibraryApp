using GAZI.LibraryApp.Data.Core;
using GAZI.LibraryApp.Data.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GAZI.LibraryApp.Data.Repositories.BooksRepositories
{
    public class BooksRepository : Books, IBooksRepository
    {
        #region Komutlar
        GenericRepository _repository;
        OracleConnection _connection;
        OracleCommand _command;

        public BooksRepository()
        {
            _command = new OracleCommand();
            _connection = OracleContext.Connection();
            _repository = new GenericRepository();
        }

        public void Add(Books book)
        {
            _repository.Add($"INSERT INTO Books(Name, AuthorID, PublisherID, TypeID, YearOfPublic, NumberOfPage, Image, Status, CreateOrModifyDate) " +
                            $"VALUES('{book.Name}', '{book.AuthorID}', '{book.PublisherID}', '{book.TypeID}', '{book.YearOfPublic}', '{book.NumberOfPage}', '{book.Image}', '{1}', '{DateTime.Now.ToString("dd.MM.yyyy")}')");

        }
        public void Delete(Books books)
        {
            //_repository.Add($"DELETE FROM Books WHERE ID == {id}");
            _repository.Add($"UPDATE Books SET Status = {books.Status}, CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}'  Where ID = {books.ID}");
        }
        public void Update(Books book)
        {
            _repository.Add($"UPDATE Books SET Name = '{book.Name}', AuthorID={book.AuthorID}, PublisherID={book.PublisherID}," +
                $" TypeID={book.TypeID}, YearOfPublic='{book.YearOfPublic}', NumberOfPage = {book.NumberOfPage}, Status = {book.Status}" +
                            $", Image = '{book.Image}', CreateOrModifyDate = '{DateTime.Now.ToString("dd.MM.yyyy")}' WHERE ID = {book.ID}");
        }

        public List<Books> GetAll()
        {
            List<Books> books = new List<Books>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Books WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Books book = new Books
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    AuthorID = Convert.ToInt32(rdr["AuthorID"]),
                    NumberOfPage = Convert.ToInt32(rdr["NumberOfPage"]),
                    TypeID = Convert.ToInt32(rdr["TypeID"]),
                    PublisherID = Convert.ToInt32(rdr["PublisherID"]),
                    YearOfPublic = rdr["YearOfPublic"].ToString(),
                    Image = rdr["Image"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])

                };
                books.Add(book);
            }
            return books;
        }

        public List<ViewBooks> GetAllLibraryBooks()
        {
            //Kütüphanede olan kitapları getirir.
            /*
              SELECT b.* FROM Books b
              WHERE NOT EXISTS(SELECT bb.BookID, bb.Status FROM BorrowBooks bb WHERE b.ID=bb.BookID AND Status=1);
             */
            List<ViewBooks> books = new List<ViewBooks>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT vb.* FROM ViewBooks vb WHERE NOT EXISTS(SELECT bb.BookID, bb.Status FROM BorrowBooks bb WHERE vb.ID=bb.BookID AND Status=1) AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                ViewBooks book = new ViewBooks
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    AuthorID = Convert.ToInt32(rdr["AuthorID"]),
                    AuthorName = rdr["AuthorName"].ToString(),
                    NumberOfPage = Convert.ToInt32(rdr["NumberOfPage"]),
                    TypeID = Convert.ToInt32(rdr["TypeID"]),
                    TypeName = rdr["TypeName"].ToString(),
                    PublisherID = Convert.ToInt32(rdr["PublisherID"]),
                    PublisherName = rdr["PublisherName"].ToString(),
                    YearOfPublic = rdr["YearOfPublic"].ToString(),
                    Image = rdr["Image"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])

                };
                books.Add(book);
            }
            return books;
        }

        public List<ViewBooks> GetAllNotLibraryBooks()
        {
            //Kütüphanede olmayan kitapları getirir.
            /*
              SELECT b.* FROM Books b
              WHERE NOT EXISTS(SELECT bb.BookID, bb.Status FROM BorrowBooks bb WHERE b.ID=bb.BookID AND Status=1);
             */
            List<ViewBooks> books = new List<ViewBooks>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT b.* FROM ViewBooks b WHERE EXISTS(SELECT bb.BookID, bb.Status FROM BorrowBooks bb WHERE b.ID=bb.BookID AND Status=1) AND Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                ViewBooks book = new ViewBooks
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    AuthorID = Convert.ToInt32(rdr["AuthorID"]),
                    AuthorName = rdr["AuthorName"].ToString(),
                    NumberOfPage = Convert.ToInt32(rdr["NumberOfPage"]),
                    TypeID = Convert.ToInt32(rdr["TypeID"]),
                    TypeName = rdr["TypeName"].ToString(),
                    PublisherID = Convert.ToInt32(rdr["PublisherID"]),
                    PublisherName = rdr["PublisherName"].ToString(),
                    YearOfPublic = rdr["YearOfPublic"].ToString(),
                    Image = rdr["Image"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])

                };
                books.Add(book);
            }
            return books;
        }

        public List<ViewBooks> GetAllView()
        {
            List<ViewBooks> books = new List<ViewBooks>();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM ViewBooks WHERE Status=1";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                ViewBooks book = new ViewBooks
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    AuthorID = Convert.ToInt32(rdr["AuthorID"]),
                    AuthorName = rdr["AuthorName"].ToString(),
                    NumberOfPage = Convert.ToInt32(rdr["NumberOfPage"]),
                    TypeID = Convert.ToInt32(rdr["TypeID"]),
                    TypeName = rdr["TypeName"].ToString(),
                    PublisherID = Convert.ToInt32(rdr["PublisherID"]),
                    PublisherName = rdr["PublisherName"].ToString(),
                    YearOfPublic = rdr["YearOfPublic"].ToString(),
                    Image = rdr["Image"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                books.Add(book);
            }
            return books;
        }

        public Books FindID(int id)
        {
            Books books = new Books();
            _command.Connection = _connection;
            _command.CommandText = $"SELECT * FROM Books WHERE ID = {id}";
            OracleDataReader rdr = _command.ExecuteReader();
            while (rdr.Read())
            {
                Books book = new Books
                {
                    ID = Convert.ToInt32(rdr["ID"]),
                    Name = rdr["Name"].ToString(),
                    AuthorID = Convert.ToInt32(rdr["AuthorID"]),
                    PublisherID = Convert.ToInt32(rdr["PublisherID"]),
                    TypeID = Convert.ToInt32(rdr["TypeID"]),
                    YearOfPublic = rdr["YearOfPublic"].ToString(),
                    NumberOfPage = Convert.ToInt32(rdr["NumberOfPage"]),
                    Image = rdr["Image"].ToString(),
                    Status = Convert.ToInt32(rdr["Status"]),
                    CreateOrModifyDate = Convert.ToDateTime(rdr["CreateOrModifyDate"])
                };
                books = book;
            }
            return books;
        }

        #endregion
    }
}
