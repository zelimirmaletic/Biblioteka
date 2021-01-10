using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Administrator : Osoba
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        public Administrator()
        {
        }

        public Administrator(string korisnickoIme, string lozinka, int IdOsoba, int IdMjesto, string Ime, string Prezime, string Adresa, string BrojTel)
            : base(IdOsoba, IdMjesto, Ime, Prezime, Adresa, BrojTel)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
        }

        public override bool Equals(object obj)
        {
            return obj is Administrator administrator &&
                   IdOsoba == administrator.IdOsoba &&
                   KorisnickoIme == administrator.KorisnickoIme;
        }

        public override int GetHashCode()
        {
            int hashCode = 1863767116;
            hashCode = hashCode * -1521134295 + IdOsoba.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KorisnickoIme);
            return hashCode;
        }

        public override string ToString()
        {
            return "ADMINISTRATOR: " + IdOsoba + " " + Ime + " " + Prezime;
        }
    }
}
