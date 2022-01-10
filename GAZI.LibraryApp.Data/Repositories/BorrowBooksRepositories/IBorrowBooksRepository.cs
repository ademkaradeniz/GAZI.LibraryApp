using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.BorrowBooksRepositories
{
    public interface IBorrowBooksRepository
    {
        void Add(BorrowBooks borrowBooks);
        void Delete(BorrowBooks borrowBooks);
        void Update(BorrowBooks borrowBooks);
        List<BorrowBooks> GetAll();
        List<ViewBorrowBooks> GetAllViewBorrowBooks();
        List<ViewBorrowBooks> GetAllUserViewBorrowBooks(int Id);
        BorrowBooks FindID(int id);

    }
}
