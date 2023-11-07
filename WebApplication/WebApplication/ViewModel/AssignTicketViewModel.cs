using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace WebApplication.ViewModel
{
    public class AssignTicketViewModel
    {
        [Display(Name = "Assign Id")]
        public int Assign_Id { get; set; }

        [Required(ErrorMessage = "Ticket Id Required")]
        [Display(Name = "Ticket Id")]
        public int Ticket_Id { get; set; }

        [Display(Name = "Department Id")]
        public int Dept_Id { get; set; }

        [Display(Name = "Assigned By")]
        public string Assigned_By { get; set; }

        [Display(Name = "Group Id")]
        public int Group_Id { get; set; }

        [Required(ErrorMessage = "Assigned To Required")]
        [Display(Name = "Assigned To")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Assigned_To { get; set; }

        [Display(Name = "Assign Priority")]
        public string Assign_Status { get; set; }

        [Required(ErrorMessage = "Response Message Required")]
        [Display(Name = "Response Message")]
        public string Response_Msg { get; set; }

        [Required(ErrorMessage = "Response Status Required")]
        [Display(Name = "Response Status")]
        public string Response_status { get; set; }
        public string Response_By { get; set; }
        public DateTime Response_Date { get; set; }

    }
}