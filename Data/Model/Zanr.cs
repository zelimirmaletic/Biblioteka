using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Zanr
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public Zanr()
        {
        }

        public Zanr(string naziv, string opis)
        {
            Naziv = naziv;
            Opis = opis;
        }

        public override bool Equals(object obj)
        {
            return obj is Zanr zanr &&
                   Naziv == zanr.Naziv;
        }

        public override int GetHashCode()
        {
            return -841901431 + Naziv.GetHashCode();
        }

        public override string ToString()
        {
            return "ZANR: " + Naziv + " " + Opis;
        }
    }
}
