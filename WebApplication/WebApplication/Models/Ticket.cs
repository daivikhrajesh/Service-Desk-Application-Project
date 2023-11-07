using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Ticket
    {
        
        [Key]
        public int Ticket_Id { get; set; }

        [Display(Name = "Ticket Description")]
        public string Ticket_Desc { get; set; }
        public string Ticket_Status { get; set; }
        public string Ticket_Priority { get; set; }
        public virtual Employee Ticket_By { get; set; }


    }
}