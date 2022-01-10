using GAZI.LibraryApp.Data.Entities;
using GAZI.LibraryApp.Data.Repositories.BorrowBooksRepositories;
using GAZI.LibraryApp.Data.Repositories.DatesRepositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Business.BorrowBooksAppService
{
    public class BorrowBooksApp : IBorrowBooksApp
    {
        [Inject]
        public IBorrowBooksRepository borrowBooksRepository { private get; set; }
        [Inject]
        public IDatesRepository datesRepository { private get; set; }

        public BorrowBooksApp(IBorrowBooksRepository _borrowBooksRepository,
            IDatesRepository _datesRepository)
        {
            borrowBooksRepository = _borrowBooksRepository;
            datesRepository = _datesRepository;
        }

        #region BorrowBooks Service

        public List<BorrowBooks> GetAllBorrowBooks()
        {
            var result = borrowBooksRepository.GetAll();
            return result;
        }

        public List<ViewBorrowBooks> GetAllViewBorrowBooks()
        {
            var result = borrowBooksRepository.GetAllViewBorrowBooks();
            return result;
        }

        public List<ViewBorrowBooks> GetAllUserViewBorrowBooks(int Id)
        {
            var result = borrowBooksRepository.GetAllUserViewBorrowBooks(Id);
            return result;
        }

        public int CreateOrUpdateDate(Dates date)
        {

            if (date != null)
            {
                if (date.ID != default(int))
                {
                    var updateDate = datesRepository.FindID(date.ID);
                    if (updateDate == null)
                    {
                        return 0;
                    }
                    updateDate.PurchaseDate = date.PurchaseDate;
                    updateDate.IssueDate = date.IssueDate;
                    updateDate.PastDate = date.PastDate;
                    updateDate.Status = 1;
                    updateDate.CreateOrModifyDate = DateTime.Now;
                    datesRepository.Update(updateDate);
                    return 0;

                }
                else
                {
                    if (datesRepository.FindDate(date).ID == default(int))
                    {
                        var entityDate = date;
                        entityDate.Status = 1;
                        entityDate.CreateOrModifyDate = DateTime.Now;
                        datesRepository.Add(entityDate);
                        return datesRepository.FindDate(date).ID;
                    }
                    else
                    {
                        return datesRepository.FindDate(date).ID;
                    }
                }
            }
            else
            {
                return 0;
            }


        }

        public bool CreateOrUpdateBorrowBook(BorrowBooks borrowBooks)
        {

            if (borrowBooks != null)
            {
                if (borrowBooks.ID != default(int))
                {
                    var updateBorrowBooks = borrowBooksRepository.FindID(borrowBooks.ID);
                    if (updateBorrowBooks == null)
                    {
                        return false;
                    }
                    updateBorrowBooks.UserID = borrowBooks.UserID;
                    updateBorrowBooks.BookID = borrowBooks.BookID;
                    updateBorrowBooks.DateID = borrowBooks.DateID;
                    updateBorrowBooks.Status = 1;
                    updateBorrowBooks.CreateOrModifyDate = DateTime.Now;
                    borrowBooksRepository.Update(updateBorrowBooks);
                    return true;

                }
                else
                {
                    var entityBorrowBook = borrowBooks;
                    entityBorrowBook.Status = 1;
                    entityBorrowBook.CreateOrModifyDate = DateTime.Now;
                    borrowBooksRepository.Add(entityBorrowBook);
                    return true;
                }
            }
            else
            {
                return false;
            }


        }

        public bool DeleteBorrowBook(int Id)
        {
            //Daha sonradan ortak tablolardan silinecek.
            if (Id != default(int))
            {
                var entityBorrowBooks = borrowBooksRepository.FindID(Id);
                if (entityBorrowBooks == null)
                {
                    return false;
                }
                entityBorrowBooks.Status = 0;
                entityBorrowBooks.CreateOrModifyDate = DateTime.Now;
                borrowBooksRepository.Delete(entityBorrowBooks);
                return true;
            }
            else
            {
                //Id değeri boş gönderilemez!
                return false;
            }
        }

        #endregion
    }
}
