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
using System.Web.Script.Serialization;
using System.Web.Security;

namespace GAZI.LibraryApp.UI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [Inject]
        public IUsersApp usersApp { set; private get; }
        [Inject]
        public IMailApp mailApp { set; private get; }

        //[Inject]
        //public IMailApp _mailApp { set; private get; }

        public LoginController(IUsersApp _usersApp,
            IMailApp _mailApp)
        {
            usersApp = _usersApp;
            mailApp = _mailApp;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Şifremi Unuttum Mail Gönderme

        [HttpPost]
        public JsonResult MailSender(string email)
        {
            if (email != null)
            {
                var user = usersApp.GetUsersEmail(email);

                if (user.ID != default(int))
                {
                    string mesaj = "<h4>Kütüphane Yönetim Sistemi</h4> <br/><br/>" +
                        "Kullanıcı Adı : " + user.UserName + " | Şifre : " + user.Password+"<br/><br/>" +
                        "Kullanıcı bilgileri ile giriş yapabilirsiniz.";
                    mailApp.MailGonder("Gazi KYS Kullanıcı Bilgileri", mesaj, email);
                }
                else
                {
                    return Json(new { result = "danger", message = "Mail sistemde kayıtlı değil !" });
                }
                               
                //Mailleri gönderdikten sonra mesaj dönüyor.
                return Json(new { result = "success", message = "Kullanıcı bilgileriniz mail olarak gönderildi." });
            }
            else
            {
                return Json(new { result = "danger", message = "Mail boş girilemez !" });
            }
        }

        #endregion

        #region Giriş Kontrol

        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            #region Validate Login

            if (ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(login.UserName) || String.IsNullOrEmpty(login.Password))
                {
                    ModelState.AddModelError("Hata:", "Email ve Şifre Alanı Boş Olamaz!");
                    //return RedirectToAction("Index", "Login");
                }

                var user = usersApp.GetUserNamePassword(login.UserName, login.Password);

                if (user.ID != default(int))
                {
                    //Code Here !.........

                    var SerializeModel = new SerializeLoginModel()
                    {
                        Id = user.ID,
                        UserName = user.UserName
                    };

                    SerializeLoginModel result = new SerializeLoginModel();
                    result.Id = user.ID;
                    result.UserName = user.UserName;


                    var serializer = new JavaScriptSerializer();
                    var userData = serializer.Serialize(SerializeModel);
                    var authTicket = new FormsAuthenticationTicket(
                        1,
                        user.UserName,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(15),
                        login.RememberMe,
                        userData);

                    var encTicket = FormsAuthentication.Encrypt(authTicket);
                    var faCookie = new HttpCookie(
                        FormsAuthentication.FormsCookieName, encTicket);
                    Response.Cookies.Add(faCookie);

                    Session.Add("UserID", user.ID);
                    Session.Add("Adi", user.Name);
                    Session.Add("Soyadi", user.SurName);

                    if (usersApp.GetRole(user.RoleID).Name == "Kullanıcı")
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else if (usersApp.GetRole(user.RoleID).Name == "Yönetici")
                    {
                        return RedirectToAction("Index", "Menager");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Login");
                    }
                }
                else
                {
                    ModelState.AddModelError("Hata:", "Kullanıcı Adı veya şifre hatalı!");
                    //return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                ModelState.AddModelError("Hata", "Login Model Doğru Tanımlanmamış!");

            }

            #endregion

            return RedirectToAction("Index", "Login");
        }


        #endregion

        #region Kullanıcı Ekle

        [HttpPost]
        public JsonResult CreateUser(Users user)
        {
            if (user != null)
            {
                
                if (usersApp.CreateLoginUser(user))
                {
                    return Json(new { result = "success", message = "Kayıt İşlemi başarılı." });

                }
                else
                {
                    return Json(new { result = "danger", message = "Kayıt işleminde hata oluştu." });
                }
            }
            else
            {
                return Json(new { result = "danger", message = "Gönderilen veri boş" });
            }
        }

        #endregion

        public ActionResult Logout()
        {
            Session.Add("UserID", null);
            Session.Add("Adi", null);
            Session.Add("Soyadi", null);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}