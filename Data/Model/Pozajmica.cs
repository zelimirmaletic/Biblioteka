using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Pozajmica
    {
        public int IdPozajmica { get; set; }
        public int IdClan { get; set; }
        public int IdKnjiga { get; set; }
        public int IdBibliotekar { get; set; }
        public DateTime DatumPozajmljivanja { get; set; }
        public bool JeRazduzena { get; set; }
        public string Opis { get; set; }
        public Pozajmica()
        {
        }

        public Pozajmica(int idPozajmica, int idClan, int idKnjiga, int idBibliotekar, DateTime datumPozajmljivanja, bool razduzena, string opis)
        {
            IdPozajmica = idPozajmica;
            IdClan = idClan;
            IdKnjiga = idKnjiga;
            IdBibliotekar = idBibliotekar;
            DatumPozajmljivanja = datumPozajmljivanja;
            JeRazduzena = razduzena;
            Opis = opis;
        }

        public override bool Equals(object obj)
        {
            return obj is Pozajmica pozajmica &&
                   IdPozajmica == pozajmica.IdPozajmica;
        }

        public override int GetHashCode()
        {
            return 649664100 + IdPozajmica.GetHashCode();
        }

        public override string ToString()
        {
            return "POZAJMICA: " + IdPozajmica + " " + IdKnjiga + " " + IdClan + " " + IdBibliotekar + " " + DatumPozajmljivanja.ToString();
        }
    }
}
