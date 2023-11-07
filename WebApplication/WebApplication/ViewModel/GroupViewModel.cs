using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModel
{
    public class GroupViewModel
    {

        [Required(ErrorMessage = "Group Id Required")]
        [Display(Name = "Group Id")]
        [Key]
        public int Group_Id { get; set; }

        [Required(ErrorMessage = "Group Name Required")]
        [Display(Name = "Group Name")]
        public string Group_Name { get; set; }



    }
}