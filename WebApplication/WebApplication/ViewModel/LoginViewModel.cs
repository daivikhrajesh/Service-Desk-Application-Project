using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModel
{
    
    public class LoginViewModel
    {
        [MinLength(6, ErrorMessage = "Username must be 6 characters")]
        [Required(ErrorMessage = "Username Required")]
        [Display(Name = "UserName")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password Required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}