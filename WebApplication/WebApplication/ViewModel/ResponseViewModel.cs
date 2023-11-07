using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebApplication.Models;

namespace WebApplication.ViewModel
{
    public class ResponseViewModel
    {

        public int Response_Id { get; set; }

        [Required(ErrorMessage = "Response Message Required")]
        [Display(Name = "Response Message")]
        public string Response_Msg { get; set; }

        [Required(ErrorMessage = "Response Status Required")]
        [Display(Name = "Response Status")]
        public string Response_status { get; set; }
        public string Response_By { get; set; }

        public int Ticket_Id { get; set; }

        [Display(Name ="Ticket Description")]
        public string Ticket_Des { get; set; }
        public int Assign_id { get; set; }
        public DateTime Response_Date { get; set; }


    }
}