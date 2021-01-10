using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Mjesto
    {

        public int IdMjesto { get; set; }
        public string Naziv { get; set; }
        public string PostanskiBroj { get; set; }

        public Mjesto()
        {
        }

        public Mjesto(int idMjesto, string naziv, string postanskiBroj)
        {
            IdMjesto = idMjesto;
            Naziv = naziv;
            PostanskiBroj = postanskiBroj;
        }

        public override bool Equals(object obj)
        {
            return obj is Mjesto mjesto &&
                   IdMjesto == mjesto.IdMjesto;
        }

        public override int GetHashCode()
        {
            return 476452762 + IdMjesto.GetHashCode();
        }

        public override string ToString()
        {
            return "MJESTO" + IdMjesto + " " + Naziv + " " + PostanskiBroj;
        }
    }
}
