using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Zanr
    {
        public int IdZanr { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public Zanr()
        {
        }

        public Zanr(int idZanr, string naziv, string opis)
        {
            IdZanr = idZanr;
            Naziv = naziv;
            Opis = opis;
        }

        public override bool Equals(object obj)
        {
            return obj is Zanr zanr &&
                   IdZanr == zanr.IdZanr;
        }

        public override int GetHashCode()
        {
            return -841901431 + IdZanr.GetHashCode();
        }

        public override string ToString()
        {
            return "ZANR: " + IdZanr + " " + Naziv + " " + Opis;
        }
    }
}
