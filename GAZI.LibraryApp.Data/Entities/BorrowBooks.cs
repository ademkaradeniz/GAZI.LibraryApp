using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Entities
{
    public class BorrowBooks
    {
        [Key]
        public int ID { get; set; }
        public int? UserID { get; set; }
        public Users Users { get; set; }
        public int? BookID { get; set; }
        public Books Books { get; set; }
        public int? DateID { get; set; }
        public Dates Dates { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateOrModifyDate { get; set; }
    }
}
