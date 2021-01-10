using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Izdavac
    {
        public Izdavac()
        {
        }

        public Izdavac(int idIzdavac, int idMjesto, string naziv, string adresa)
        {
            IdIzdavac = idIzdavac;
            IdMjesto = idMjesto;
            Naziv = naziv;
            Adresa = adresa;
        }

        public int IdIzdavac {get; set;}
        public int IdMjesto { get; set; }

        public string Naziv { get; set; }
        public string Adresa { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Izdavac izdavac &&
                   IdIzdavac == izdavac.IdIzdavac;
        }

        public override int GetHashCode()
        {
            return 1068385738 + IdIzdavac.GetHashCode();
        }

        public override string ToString()
        {
            return "IZDAVAC: " + IdIzdavac + " " + IdMjesto + " " + Naziv + " " + Adresa;
        }
    }
}
