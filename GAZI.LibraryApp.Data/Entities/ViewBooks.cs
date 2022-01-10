using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Entities
{
    public class ViewBooks
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int? AuthorID { get; set; }
        public string AuthorName { get; set; }
        public int? PublisherID { get; set; }
        public string PublisherName { get; set; }
        public int? TypeID { get; set; }
        public string TypeName { get; set; }
        public string YearOfPublic { get; set; }
        public int? NumberOfPage { get; set; }
        public string Image { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateOrModifyDate { get; set; }
    }
}
