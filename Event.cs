using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace PIDEV_NET.Models
{
    public class Event
    {
        public long id { get; set; }
    
        public string name { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        [DataType(DataType.Date)]
        public DateTime addDate { get; set; }
        
    }
}