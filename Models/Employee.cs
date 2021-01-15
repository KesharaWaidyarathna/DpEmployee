using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DpEmployee.Models
{
    public class Employee
    {
        [Key]
        [MaxLength(3)]
        public int Code { get; set; }

        public string Initials { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string Surname { get; set; }

        [Required]
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public bool Status { get; set; }

        public string FdFirstName { get; set; }

        public string FdSurname { get; set; }

        public string FdRelationShip { get; set; }
    }
}
