using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Bibliotekar
    {
        public int IdBibliotekar { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        public Bibliotekar()
        {
        }

        public Bibliotekar(int idBibliotekar, string korisnickoIme, string lozinka)
        {
            IdBibliotekar = idBibliotekar;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
        }

        public override bool Equals(object obj)
        {
            return obj is Bibliotekar bibliotekar &&
                   IdBibliotekar == bibliotekar.IdBibliotekar &&
                   KorisnickoIme == bibliotekar.KorisnickoIme;
        }

        public override int GetHashCode()
        {
            int hashCode = 1863767116;
            hashCode = hashCode * -1521134295 + IdBibliotekar.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KorisnickoIme);
            return hashCode;
        }

        public override string ToString()
        {
            return "BIBLIOTEKAR: "+IdBibliotekar + " " + KorisnickoIme; 
        }
    }
}
