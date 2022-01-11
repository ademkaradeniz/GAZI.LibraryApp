using GAZI.LibraryApp.Business.BooksAppService;
using GAZI.LibraryApp.Business.BorrowBooksAppService;
using GAZI.LibraryApp.Business.MailAppService;
using GAZI.LibraryApp.Business.UsersAppService;
using GAZI.LibraryApp.Data.Entities;
using GAZI.LibraryApp.UI.Auth;
using GAZI.LibraryApp.UI.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAZI.LibraryApp.UI.Controllers
{
    public class UserController : Controller
    {
        // GET: User

        [Inject]
        IBooksApp booksApp { set; get; }

        [Inject]
        IUsersApp usersApp { get; set; }

        [Inject]
        IBorrowBooksApp borrowBooksApp { get; set; }
       
        [Inject]
        IMailApp mailApp { get; set; }

        public UserController(IBooksApp _booksApp,
            IUsersApp _usersApp,
            IBorrowBooksApp _borrowBooksApp,
            IMailApp _mailApp)
        {
            booksApp = _booksApp;
            usersApp = _usersApp;
            borrowBooksApp = _borrowBooksApp;
            mailApp = _mailApp;
        }

        public ActionResult Information()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Kullanıcı")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.User = usersApp.GetUser(int.Parse(Session["UserID"].ToString()));
                return View();
            }
        }

        public ActionResult Password()
        {

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Kullanıcı")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.User = usersApp.GetUser(int.Parse(Session["UserID"].ToString()));
                return View();
            }
        }

        #region Şifremi Unuttum Mail Gönderme

        [HttpPost]
        public JsonResult MailSender(int Id)
        {
            if (Session["UserID"] == null)
            {
                return Json(new { result = "danger", message = "Mail gönderilemedi. Oturumunuz sonlanmış olabilir !" });
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Kullanıcı")
            {
                return Json(new { result = "danger", message = "Mail sistemde kayıtlı değil !" });
            }
            else
            {
                var user = usersApp.GetUser(int.Parse(Session["UserID"].ToString()));
                string mesaj = "<h4>Kütüphane Yönetim Sistemi</h4> <br/><br/>" +
                        "Kullanıcı Adı : " + user.UserName + " | Şifre : " + user.Password + "<br/><br/>" +
                        "Kullanıcı bilgileri ile giriş yapabilirsiniz.";
                mailApp.MailGonder("Gazi KYS Kullanıcı Bilgileri", mesaj, user.Email);
                //Mailleri gönderdikten sonra mesaj dönüyor.
                return Json(new { result = "success", message = "Kullanıcı bilgileriniz mail olarak gönderildi." });
            }
        }

        #endregion

        [HttpPost]
        public JsonResult UpdateUser(Users user)
        {
            if (user != null)
            {

                if (usersApp.UpdateUser(user))
                {
                    return Json(new { result = "success", message = "Güncelleme İşlemi başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Güncelleme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        public ActionResult Index()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Kullanıcı")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                //Ön yüz bilgilerini getirir.
                MenagerIndexModel model = new MenagerIndexModel();
                model.TotalUsers = usersApp.GetAllUsers().Count;
                model.UserRoles = usersApp.GetAllRoles().Count;
                model.TotalBooks = booksApp.GetAllBooks().Count;
                model.LibraryBooks = booksApp.GetAllLibraryBooks().Count;
                model.BorrowBooks = booksApp.GetAllNotLibraryBooks().Count;

                ViewBag.Information = model;
                return View();
            }
        }

        public ActionResult LibraryBookList()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Kullanıcı")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.LibraryBooks = booksApp.GetAllLibraryBooks();
                ViewBag.User = usersApp.GetUser(int.Parse(Session["UserID"].ToString()));
                return View();
            }
        }

        public ActionResult LendBook()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Kullanıcı")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.LibraryBooks = booksApp.GetAllLibraryBooks();
                ViewBag.User = usersApp.GetUser(int.Parse(Session["UserID"].ToString()));
                return View();
            }
        }

        public ActionResult ReturnBook()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Kullanıcı")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.UserBorrowBooks = borrowBooksApp.GetAllUserViewBorrowBooks(int.Parse(Session["UserID"].ToString()));
                return View();
            }
        }

        #region JSON Service

        [HttpPost]
        public JsonResult CreateOrUpdateBorrowBook(BorrowBooks borrowBooks, Dates date)
        {
            if (borrowBooks != null && date != null)
            {
                var dateID = borrowBooksApp.CreateOrUpdateDate(date);
                if (dateID != default(int))
                {
                    borrowBooks.DateID = dateID;
                    if (borrowBooksApp.CreateOrUpdateBorrowBook(borrowBooks))
                    {
                        return Json(new { result = "success", message = "İşlem başarılı." });

                    }
                    else
                    {
                        return Json(new { result = "danger", message = "Kitap verme işleminde hata oluştu." });
                    }
                }
                else
                {
                    return Json(new { result = "danger", message = "Kitap verme işleminde hata oluştu." });
                }

            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        [HttpPost]
        public JsonResult CreateOrUpdateReturnBook(int Id)
        {
            if (Id != default(int))
            {
                if (borrowBooksApp.DeleteBorrowBook(Id))
                {
                    return Json(new { result = "success", message = "Kitap başarılı şekilde geri alındı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "İşlem sırasında bir hata oluştu!" });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        [HttpPost]
        public JsonResult GetUser(int Id)
        {
            if (Id != default(int))
            {
                var data = usersApp.GetUser(Id);
                if (data != null)
                {
                    return Json(new { result = "success", message = "Kullanıcı Bilgileri Getirildi.", veri = data });

                }
                else
                {
                    return Json(new { result = "danger", message = "Kullanıcı Bilgileri bulunamadı!" });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Kullanıcı Seçilmedi!" });
            }
        }

        #endregion
    }
}