using GAZI.LibraryApp.Business.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Business.MailAppService
{
    public class MailApp : IMailApp
    {
        public bool MailGonder(string konu, string mesaj, string eposta)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential("gazikursyonetimsistemi@gmail.com", "Gazi1234*"); // Gönderici bilgilerini giriyoruz
                smtp.Port = 587; // Mail uzantınıza göre bu değişebilir
                smtp.Host = "smtp.gmail.com"; // Gmail veya hotmail ise onların host bilgisini almanız gerekiyor 
                smtp.EnableSsl = true;
                mail.IsBodyHtml = true;// HTML tagleri kullanarak mail gönderebilmek için aktif ediyoruz
                mail.From = new MailAddress("gazikursyonetimsistemi@gmail.com"); // Gönderen mail adresini yazıyoruz
                mail.To.Add(eposta); // Gönderilecek mail adresini ekliyoruz
                mail.Subject = konu; // Mesaja konuyu ekliyoruz
                mail.Body = mesaj; // Mesajın içeriğini ekliyoruz

                smtp.Send(mail); // Mesajı gönderiyoruz
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
