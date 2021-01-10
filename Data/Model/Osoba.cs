using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    abstract class Osoba
    {
        public int IdOsoba { get; set; }
        public int IdMjesto { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }

        protected Osoba()
        {
        }
        protected Osoba(int idOsoba, int idMjesto, string ime, string prezime, string adresa, string brojTelefona)
        {
            IdOsoba = idOsoba;
            IdMjesto = idMjesto;
            Ime = ime;
            Prezime = prezime;
            Adresa = adresa;
            BrojTelefona = brojTelefona;
        }



    }
}
