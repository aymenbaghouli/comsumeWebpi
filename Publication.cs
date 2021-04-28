using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pi.Models
{
    public class Publication
    {
        public int idPub { get; set; }
        public String text { get; set; }
        public String photo { get; set; }
        //public DateTime DatePub { get; set; }

        public forum forum { get; set; }
        public List<comment> comments { get; set; }

    }
}