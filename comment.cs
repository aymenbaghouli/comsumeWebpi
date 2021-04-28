using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pi.Models
{
    public class comment
    {
        public long idcomment { get; set; }
        public String text { get; set; }
        public String photo { get; set; }
        public String reaction { get; set; }
       // public Boolean likes { get; set; }

        public Publication Publications { get; set; }


        public comment(long Id, string texte, String pic, String react)
        {
            idcomment = Id;
            text = texte;
            photo = pic;
            reaction = react;
            //likes = like;
        }
        public comment()
        {

        }



    }
}