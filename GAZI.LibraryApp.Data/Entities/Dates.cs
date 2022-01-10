using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Entities
{
    public class Dates
    {
        [Key]
        public int ID { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? PastDate { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateOrModifyDate { get; set; }
        public List<BorrowBooks> BorrowBooks { get; set; }
    }
}
