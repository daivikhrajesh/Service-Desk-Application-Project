using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Assign_Ticket
    {
        [Key]
        public int Assign_Id { get; set; }
        public int Ticket_Id { get; set; }
        public int Dept_Id { get; set; }
        public string Assigned_By { get; set; }
        public int Group_Id { get; set; }
        public string Assigned_To { get; set; }
        public string Assign_Status { get; set; }

        

    }
}