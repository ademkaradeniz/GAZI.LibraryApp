using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Business.BorrowBooksAppService
{
    public interface IBorrowBooksApp
    {
        /// <summary>
        /// Tüm emanetteki kitap ve kullanıcı bilgilerini getirir.
        /// </summary>
        /// <returns></returns>
        List<BorrowBooks> GetAllBorrowBooks();

        /// <summary>
        /// Tüm emanetteki kitap ve kullanıcı bilgilerini ve isimlerini getirir.
        /// </summary>
        /// <returns></returns>
        List<ViewBorrowBooks> GetAllViewBorrowBooks();

        /// <summary>
        /// Kullanıcı Id'si gönderilen kullanıcının ödünç aldığı kitapları getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<ViewBorrowBooks> GetAllUserViewBorrowBooks(int Id);

        /// <summary>
        /// Tarihi kaydeder ve id'sini getirir.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        int CreateOrUpdateDate(Dates date);

        /// <summary>
        /// Yeni bir emanet işlemi oluşturur.
        /// </summary>
        /// <param name="borrowBooks"></param>
        /// <returns></returns>
        bool CreateOrUpdateBorrowBook(BorrowBooks borrowBooks);

        /// <summary>
        /// Emanet işlemini siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteBorrowBook(int Id);
    }
}
