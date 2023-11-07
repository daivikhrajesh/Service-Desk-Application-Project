using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WebApplication.ViewModel
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "Employee Id Required")]
        [Display(Name = "Employee Id")]
        public int Emp_Id { get; set; }

        [Required(ErrorMessage = "Employee Name Required")]
        [Display(Name = "Employee Name")]
        public string Emp_Name { get; set; }

        
        [Display(Name = "Email Id")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email_Id { get; set; }

        [Required(ErrorMessage = "Group Id  Required")]
        [Display(Name = "Group Id")]
        public int Group_Id { get; set; }

        [Required(ErrorMessage = "Group Name  Required")]
        [Display(Name = "Group Name")]
        public string Group_Name { get; set; }

        [Required(ErrorMessage = "Department Required")]
        [Display(Name = "Department Name")]
        public string Department_Name { get; set; }

        [Required(ErrorMessage = "Employee Phone Required")]
        [Display(Name = "Employee Phone")]
        public int Emp_Phone { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "Employee Password")]
        public string Emp_Password { get; set; }

        [Required(ErrorMessage = "Role Name Required")]
        [Display(Name = "Role Name")]
        public string Role_Name { get; set; }

    }
}