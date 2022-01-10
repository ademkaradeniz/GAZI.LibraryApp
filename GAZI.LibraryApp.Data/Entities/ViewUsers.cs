using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Entities
{
    public class ViewUsers
    {
        [Key]
        public int ID { get; set; }
        public int? RoleID { get; set; }
        public string RoleName { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AuthCode { get; set; }
        public string Telephone { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateOrModifyDate { get; set; }
    }
}
