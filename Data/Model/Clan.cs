using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Clan : Osoba
    {
        public DateTime DatumUclanjivanja { get; set; }
        public DateTime DatumObnavljanjaClanstva { get; set; }
        public string Email { get; set; }

        public Clan()
        {
        }

        public Clan(DateTime datumUclanjivanja, DateTime datumOnavljanja, int IdOsoba, int IdMjesto, string Ime, string Prezime, string Adresa, string BrojTel)
            : base(IdOsoba, IdMjesto, Ime, Prezime, Adresa, BrojTel)
        {
            DatumUclanjivanja = datumUclanjivanja;
            DatumObnavljanjaClanstva = datumOnavljanja;
        }

        public override bool Equals(object obj)
        {
            return obj is Clan clan &&
                   IdOsoba == clan.IdOsoba;
        }

        public override int GetHashCode()
        {
            return 1850017762 + IdOsoba.GetHashCode();
        }

        public override string ToString()
        {
            return "OSOBA: " + IdOsoba + " " + Ime + " " + Prezime;
        }
    }
}
