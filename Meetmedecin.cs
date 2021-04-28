using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class Meetmedecin
    {

      
        public int idmeetmed { get; set; }


        [Required]
        public String datemeet { get; set; }
        public String maladie { get; set; }
       
        public String madecin { get; set; }
        public String enfant { get; set; }
        public String classe { get; set; }







    }
}