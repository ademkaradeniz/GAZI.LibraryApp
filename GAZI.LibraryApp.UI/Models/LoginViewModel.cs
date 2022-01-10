using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GAZI.LibraryApp.UI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Boş Geçilmez!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Boş Geçilmez!")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}