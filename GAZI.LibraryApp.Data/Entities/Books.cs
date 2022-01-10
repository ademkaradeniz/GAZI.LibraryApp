using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Entities
{
    public class Books
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int? AuthorID { get; set; }
        public Authors Author { get; set; }
        public int? PublisherID { get; set; }
        public Publishers Publishers { get; set; }
        public int? TypeID { get; set; }
        public Types Types { get; set; }
        public string YearOfPublic { get; set; }
        public int? NumberOfPage { get; set; }
        public string Image { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateOrModifyDate { get; set; }
        public List<BorrowBooks> BorrowBooks { get; set; }
    }
}
