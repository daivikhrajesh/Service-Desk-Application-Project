using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Group
    {
        [Key]
        public int Group_Id { get; set; }

        public string Group_Name { get; set; }

    }
}