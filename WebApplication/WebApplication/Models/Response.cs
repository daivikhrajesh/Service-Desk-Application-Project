using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Response
    {
        [Key]
        public int Response_Id { get; set; }
        public string Response_Msg { get; set; }
        public string Response_status { get; set; }
        public DateTime Response_Date { get; set; }
        public int Ticket_Id { get; set; }

    }
}