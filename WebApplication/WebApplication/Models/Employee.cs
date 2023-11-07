using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{

    public class Employee
    {
        [Key]
        public int Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Email_Id { get; set; }      
        public virtual Group Group { get; set; }       
        public virtual Department Department { get; set; }
        public int Emp_Phone { get; set; }
        public string Emp_Password { get; set; }
        public virtual Role Role { get; set; }

    }
}