using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAZI.LibraryApp.UI.Auth
{
    [Serializable]
    public class SerializeLoginModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}