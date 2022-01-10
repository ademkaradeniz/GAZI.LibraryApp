using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Entities
{
    public class Types
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateOrModifyDate { get; set; }
        public List<Books> Books { get; set; }
    }
}
