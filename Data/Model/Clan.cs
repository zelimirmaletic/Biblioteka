using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Clan
    {
        public int IdClan { get; set; }
        public DateTime DatumUclanjivanja { get; set; }
        public DateTime DatumObnavljanjaClanstva { get; set; }

        public Clan()
        {
        }

        public Clan(int idClan, DateTime datumUclanjivanja, DateTime datumOnavljanja)
        {
            IdClan = idClan;
            DatumUclanjivanja = datumUclanjivanja;
            DatumObnavljanjaClanstva = datumOnavljanja;
        }

        public override bool Equals(object obj)
        {
            return obj is Clan clan &&
                   IdClan == clan.IdClan;
        }

        public override int GetHashCode()
        {
            return 1850017762 + IdClan.GetHashCode();
        }

        public override string ToString()
        {
            return "CLAN: " + IdClan + " " + DatumUclanjivanja.ToString();
        }
    }
}
