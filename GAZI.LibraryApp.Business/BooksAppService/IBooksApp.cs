using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Business.BooksAppService
{
    public interface IBooksApp
    {
        #region Authors Service

        /// <summary>
        /// Tüm yazarları getirir.
        /// </summary>
        /// <returns></returns>
        List<Authors> GetAllAuthors();

        /// <summary>
        /// Yeni yazar ekler veya var olanı günceller.
        /// </summary>
        /// <param name="authors"></param>
        /// <returns></returns>
        bool CreateOrUpdateAuthor(Authors authors);

        /// <summary>
        /// Id'si gönderilen yazarı siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteAuthor(int Id);

        #endregion

        #region Publishers Service

        /// <summary>
        /// Tüm yayınevlerini getirir.
        /// </summary>
        /// <returns></returns>
        List<Publishers> GetAllPublishers();

        /// <summary>
        /// Yeni yayınevi ekler veya var olanı günceller.
        /// </summary>
        /// <param name="publishers"></param>
        /// <returns></returns>
        bool CreateOrUpdatePublisher(Publishers publishers);

        /// <summary>
        /// Id'si gönderilen yayınevini siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeletePublisher(int Id);

        #endregion

        #region Types Service

        /// <summary>
        /// Tüm kitap türlerini getirir.
        /// </summary>
        /// <returns></returns>
        List<Types> GetAllTypes();

        /// <summary>
        /// Yeni kitap türü ekler veya var olanı günceller.
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        bool CreateOrUpdateType(Types types);

        /// <summary>
        /// Id'si gönderilen kitap türünü siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteType(int Id);

        #endregion

        #region Books Service

        /// <summary>
        /// Tüm kitap listelerini getirir.
        /// </summary>
        /// <returns></returns>
        List<Books> GetAllBooks();

        /// <summary>
        /// Kütüphanede olan kitapları getirir.
        /// </summary>
        /// <returns></returns>
        List<ViewBooks> GetAllLibraryBooks();

        /// <summary>
        /// Kütüphanede olmayan kitapları getirir.
        /// </summary>
        /// <returns></returns>
        List<ViewBooks> GetAllNotLibraryBooks();

        /// <summary>
        /// Tüm kitap listesini bağlı olduğu tablolar ile join ederek getirir.
        /// </summary>
        /// <returns></returns>
        List<ViewBooks> GetAllViewBooks();

        /// <summary>
        /// Yeni bir kitap ekler veya var olanı günceller.
        /// </summary>
        /// <param name="books"></param>
        /// <returns></returns>
        bool CreateOrUpdateBook(Books books);

        /// <summary>
        /// Kitabı kütüphaneden siler.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool DeleteBook(int Id);

        #endregion
    }
}
