using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModel
{
    public class DepartmentViewModel
    {

        [Required(ErrorMessage = "Department Id Required")]
        [Display(Name = "Department Id")]
        [Key]
        public int Dept_Id { get; set; }

        [Required(ErrorMessage = "Department Name Required")]
        [Display(Name = "Department Name")]
        public string Dept_Name { get; set; }

        
    }
}