using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Business.MailAppService
{
    public interface IMailApp
    {
        bool MailGonder(string konu, string mesaj, string eposta);
    }
}
