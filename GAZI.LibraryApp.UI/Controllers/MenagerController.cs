using GAZI.LibraryApp.Business.BooksAppService;
using GAZI.LibraryApp.Business.BorrowBooksAppService;
using GAZI.LibraryApp.Business.MailAppService;
using GAZI.LibraryApp.Business.UsersAppService;
using GAZI.LibraryApp.Data.Entities;
using GAZI.LibraryApp.Data.Repositories.BooksRepositories;
using GAZI.LibraryApp.UI.Auth;
using GAZI.LibraryApp.UI.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GAZI.LibraryApp.UI.Controllers
{
    public class MenagerController : Controller
    {
        // GET: Yonetici
        [Inject]
        IBooksApp booksApp { set; get; }

        [Inject]
        IUsersApp usersApp { get; set; }

        [Inject]
        IBorrowBooksApp borrowBooksApp { get; set; }

        [Inject]
        IMailApp mailApp { get; set; }

        public MenagerController(IBooksApp _booksApp,
            IUsersApp _usersApp,
            IBorrowBooksApp _borrowBooksApp,
            IMailApp _mailApp)
        {
            booksApp = _booksApp;
            usersApp = _usersApp;
            borrowBooksApp = _borrowBooksApp;
            mailApp = _mailApp;
        }

        public ActionResult Index()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
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

        public ActionResult Information()
        {

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
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
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.User = usersApp.GetUser(int.Parse(Session["UserID"].ToString()));
                return View();
            }
        }

       
        [HttpPost]
        public JsonResult MailPasswordSender(int Id)
        {
            if (Session["UserID"] == null)
            {
                return Json(new { result = "danger", message = "Mail gönderilemedi. Oturumunuz sonlanmış olabilir !" });
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
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

        #region Kitap İşlemleri

        public ActionResult BookList()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Authors = booksApp.GetAllAuthors();
                ViewBag.Publishers = booksApp.GetAllPublishers();
                ViewBag.Types = booksApp.GetAllTypes();
                ViewBag.Books = booksApp.GetAllViewBooks();
                return View();
            }
        }

        [HttpPost]
        public JsonResult CreateOrUpdateBook(Books book)
        {
            if (book != null)
            {
                if (booksApp.CreateOrUpdateBook(book))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Kitap ekleme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        [HttpPost]
        public JsonResult DeleteBook(int Id)
        {
            if (Id != default(int))
            {
                if (booksApp.DeleteBook(Id))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Kitap silme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        #endregion

        #region Yazar İşlemleri

        public ActionResult AuthorList()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Authors = booksApp.GetAllAuthors();
                return View();
            }
        }

        [HttpPost]
        public JsonResult CreateOrUpdateAuthor(Authors author)
        {
            if (author != null)
            {
                if (booksApp.CreateOrUpdateAuthor(author))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Yazar ekleme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        [HttpPost]
        public JsonResult DeleteAuthor(int Id)
        {
            if (Id != default(int))
            {
                if (booksApp.DeleteAuthor(Id))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Yazar silme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        #endregion

        #region Yayınevi İşlemleri

        public ActionResult PublisherList()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Publishers = booksApp.GetAllPublishers();
                return View();
            }
        }

        [HttpPost]
        public JsonResult CreateOrUpdatePublisher(Publishers publisher)
        {
            if (publisher != null)
            {
                if (booksApp.CreateOrUpdatePublisher(publisher))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Yayınevi ekleme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        [HttpPost]
        public JsonResult DeletePublisher(int Id)
        {
            if (Id != default(int))
            {
                if (booksApp.DeletePublisher(Id))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Yayınevi silme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        #endregion

        #region Kitap Türleri İşlemleri

        public ActionResult TypeList()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Types = booksApp.GetAllTypes();
                return View();
            }
        }

        [HttpPost]
        public JsonResult CreateOrUpdateType(Types type)
        {
            if (type != null)
            {
                if (booksApp.CreateOrUpdateType(type))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Kitap türü ekleme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        [HttpPost]
        public JsonResult DeleteType(int Id)
        {
            if (Id != default(int))
            {
                if (booksApp.DeleteType(Id))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Kitap türü silme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        #endregion

        #region Kullanıcı İşlemleri

        public ActionResult UserList()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Roles = usersApp.GetAllRoles();
                ViewBag.Users = usersApp.GetAllViewUsers();
                return View();
            }
        }

        [HttpPost]
        public JsonResult CreateOrUpdateUser(Users user)
        {
            if (user != null)
            {
                if (usersApp.CreateOrUpdateUser(user))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Kullanıcı ekleme işleminde hata oluştu. Email ve UserName farklı kullanıcıda kullanılıyor olabilir." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        [HttpPost]
        public JsonResult DeleteUser(int Id)
        {
            if (Id != default(int))
            {
                if (usersApp.DeleteUser(Id))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Kullanıcı silme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        #endregion

        #region Rol İşlemleri

        public ActionResult RoleList()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.Roles = usersApp.GetAllRoles();
                return View();
            }
        }

        [HttpPost]
        public JsonResult CreateOrUpdateRole(Roles role)
        {
            if (role != null)
            {
                if (usersApp.CreateOrUpdateRole(role))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Rol ekleme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        [HttpPost]
        public JsonResult DeleteRole(int Id)
        {
            if (Id != default(int))
            {
                if (usersApp.DeleteRole(Id))
                {
                    return Json(new { result = "success", message = "İşlem başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Rol silme işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        #endregion

        #region Ödünç İşlemleri

        public ActionResult BorrowBookList()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.BorrowBooks = borrowBooksApp.GetAllViewBorrowBooks();
                return View();
            }
        }

        #region Hatırlatma Mail Gönderme

        [HttpPost]
        public JsonResult MailSender(ViewBorrowBooks viewborrowBooks,string AlisTarih,string VerilecekTarih)
        {
            if (viewborrowBooks != null)
            {
                if (viewborrowBooks.Email != null)
                {
                    string mesaj = "Kütüphane Yönetim Sistemi<br/><br/>" +
                        "Kitap Bilgileri<br/>" +
                        "Kitap Adı : " + viewborrowBooks.BookName + " | Yazar : " + viewborrowBooks.AuthorName + "<br/>" +
                        "Yayınevi : " + viewborrowBooks.PublisherName + " | Türü : " + viewborrowBooks.TypeName + "<br/><br/>" +
                        "Kullanıcı Bilgileri<br/>" +
                        "Adı : " + viewborrowBooks.Name + " | Soyadı : " + viewborrowBooks.SurName + "<br/><br/>" +
                        "Ödünç Alınan Tarih Bilgileri<br/>" +
                        "Kitap Alış Tarih : " + AlisTarih + " | Kitap Geri Verilecek Tarih : " + VerilecekTarih + "<br/><br/>" +
                        "Bilgileri verilen kitabı " + VerilecekTarih + " tarihinde kütüphaneye bırakmanızı hatırlatırız.<br/>" +
                        "İyi okumalar...";
                    mailApp.MailGonder("Gazi KYS Kullanıcı Bilgileri", mesaj, "adem_karadeniz@outlook.com");
                }
                else
                {
                    return Json(new { result = "danger", message = "Mail sistemde kayıtlı değil !" });
                }

                //Mailleri gönderdikten sonra mesaj dönüyor.
                return Json(new { result = "success", message = "Hatırlatma maili kullanıcıya gönderildi." });
            }
            else
            {
                return Json(new { result = "danger", message = "Mail boş girilemez !" });
            }
        }

        #endregion

        public ActionResult LendBook()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.LibraryBooks = booksApp.GetAllLibraryBooks();
                ViewBag.Users = usersApp.GetAllUsers();
                return View();
            }
        }

        public ActionResult ReturnBook()
        {
            
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if (usersApp.GetUser(int.Parse(Session["UserID"].ToString())) == null || usersApp.GetRole(usersApp.GetUser(int.Parse(Session["UserID"].ToString())).RoleID).Name != "Yönetici")
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.BorrowBooks = borrowBooksApp.GetAllViewBorrowBooks();
                return View();
            }
        }

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
                    return Json(new { result = "danger", message = "Kullanıcı Bilgileri bulunamadı!"});
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Kullanıcı Seçilmedi!"});
            }
        }

        #endregion

        [HttpPost]
        public JsonResult Upload()
        {
            string path, fName = "BookImage_" + DateTime.Now.ToString("ddMMyyyyHHmmss");

            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                if (file != null && file.ContentLength > 0)
                {

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Content\\Images\\Books", Server.MapPath(@"\")));

                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "");

                    var fileName1 = Path.GetFileName(fName);

                    bool isExists = System.IO.Directory.Exists(pathString);

                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);

                    path = string.Format("{0}\\{1}", pathString, fName);
                    try
                    {
                        WebImage img = new WebImage(file.InputStream);
                        if (img.Width != 0 || img.Height != 0)
                        {
                            img.Save(path);
                            return Json("/Content/Images/Books/" + fName + "." + img.ImageFormat, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json("Error", JsonRequestBehavior.AllowGet);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json("Error", JsonRequestBehavior.AllowGet);
                    }

                }

            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
    }
}