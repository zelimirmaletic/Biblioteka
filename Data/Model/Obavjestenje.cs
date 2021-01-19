using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Obavjestenje
    {
        public int IdObavjestenje{get;set;}
        public int IdAdministrator { get; set; }
        public int IdBibliotekar { get; set; }
        public string Naslov { get; set; }
        public DateTime Datum { get; set; }
        public string Tekst { get; set; }

        public bool ZaSve { get; set; }

        public Obavjestenje()
        {
        }

        public Obavjestenje(int idAdministrator, int idBibliotekar, string naslov, DateTime datum, string tekst)
        {
            IdAdministrator = idAdministrator;
            IdBibliotekar = idBibliotekar;
            Naslov = naslov;
            Datum = datum;
            Tekst = tekst;
        }

        public override bool Equals(object obj)
        {
            return obj is Obavjestenje obavjestenje &&
                   IdObavjestenje == obavjestenje.IdObavjestenje;
        }

        public override int GetHashCode()
        {
            return -735747090 + IdObavjestenje.GetHashCode();
        }
    }
}
