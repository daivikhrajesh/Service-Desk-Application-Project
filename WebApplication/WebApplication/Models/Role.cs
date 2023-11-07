using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Role
    {
        [Key]
        public int Role_Id { get; set; }

        public string Role_Name { get; set; }

    }
}