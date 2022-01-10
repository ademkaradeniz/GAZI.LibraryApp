using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.BooksRepositories
{
    public interface IBooksRepository
    {
        void Add(Books book);
        void Delete(Books books);
        void Update(Books book);
        List<Books> GetAll();
        List<ViewBooks> GetAllLibraryBooks();
        List<ViewBooks> GetAllNotLibraryBooks();
        List<ViewBooks> GetAllView();
        Books FindID(int id);
    }
}
