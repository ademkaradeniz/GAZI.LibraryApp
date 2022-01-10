using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Entities
{
    public class ViewBorrowBooks
    {
        [Key]
        public int ID { get; set; }
        public int? UserID { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int? BookID { get; set; }
        public string BookName { get; set; }
        public int? AuthorID { get; set; }
        public string AuthorName { get; set; }
        public int? PublisherID { get; set; }
        public string PublisherName { get; set; }
        public int? TypeID { get; set; }
        public string TypeName { get; set; }
        public string YearOfPublic { get; set; }
        public int? NumberOfPage { get; set; }
        public string Image { get; set; }
        public int? DateID { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? PastDate { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateOrModifyDate { get; set; }
    }
}
