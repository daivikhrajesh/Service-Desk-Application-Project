﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Department
    {
        
        [Key]
        public int Dept_Id { get; set; }

    
        public string Dept_Name { get; set; }


    }
}