using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAZI.LibraryApp.UI.Models
{
    public class MenagerIndexModel
    {
        public int TotalBooks { get; set; }

        public int LibraryBooks { get; set; }

        public int BorrowBooks { get; set; }

        public int TotalUsers { get; set; }

        public int UserRoles { get; set; }

    }
}