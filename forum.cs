using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pi.Models
{
    public class forum
    {
        public long Id { get; set; }
        public String title { get; set; }
        public String pic  { get; set; }
        public String description { get; set; }

        public Categorie Categorie { get; set; }
        public List<Publication> Publications { get; set; }

        public forum(long IdForum, string titleForum, String picForum, String descriptionForum)
        {
            Id = IdForum;
            title = titleForum;
            pic = picForum;
            description = descriptionForum;
        }
        public forum()

        { }
       

    }


}
