using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;






namespace WebApplication2.Models
{
    public class Meeting
    {
        public int idmeet { get; set; }


       
        [Required]
        public String datemeet { get; set; }
        public String subject { get; set; }
        public String description { get; set; }
        public String sender { get; set; }
        public String classe { get; set; }
    }
}