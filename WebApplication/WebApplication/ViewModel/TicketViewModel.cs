using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.ViewModel
{
    public class TicketViewModel
    {
        [Display(Name = "Ticket Id")]
        [Required(ErrorMessage = "Ticket Id Required")]
        public int Ticket_Id { get; set; }

        [MaxLength(1000)]
        [Display(Name = "Ticket Description")]
        [Required(ErrorMessage = "Ticket Description Required")]
        [StringLength(1000, MinimumLength = 25, ErrorMessage = "Ticket description must be between 25 and 1000 characters")]
        public string Ticket_Desc { get; set; }

        [Required(ErrorMessage = "Ticket Status Required")]
        [Display(Name = "Ticket Status")]
        public string Ticket_Status { get; set; }

        [Required(ErrorMessage = "Ticket Priority Required")]
        [Display(Name = "Ticket Priority")]
        public string Ticket_Priority { get; set; }

        [Required(ErrorMessage = "Employee Id Required")]
        [Display(Name = "Employee Id")]
        public int Emp_Id { get; set; }

        public string response { get; set; }
    }
    

}