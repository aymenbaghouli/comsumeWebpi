using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pi.Models
{
    public class Categorie
    {
        
        public long idCat { get; set; }
        public String NomCat { get; set; }

        public List<forum> forums { get; set; }

        public Categorie(long id,string nom)
        {
            idCat = id;
            NomCat = nom;
        }
        public Categorie()

        { }
        public override string ToString()
        {
            return idCat + NomCat;
        }

    }
}