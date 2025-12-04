using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace consoleApp
{
    public class DonneesUtilisateur
    {
        public int ID { get; set; }
        public string Nom              { get; set; }
        public string MotDePasse   { get; set; }
    }

    public class DonneesNote
    {
        public int ID { get; set; }
        public string Contenu              { get; set; }
        public int UserID              { get; set; }
    }

}
