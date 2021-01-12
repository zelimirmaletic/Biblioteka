using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Knjiga
    {
        public int IdKnjiga { get; set; }
        public int IdZanr { get; set; }
        public int IdIzdavac { get; set; }
        public string Naslov { get; set; }
        public DateTime DatumObjavljivanja { get; set; }
        public string ISBN { get; set; }
        public int UkupanBrojKopija { get; set; }
        public int BrojStranica { get; set; }
        public string Jezik { get; set; }
        public string Opis { get; set; }

        public Knjiga(int idKnjiga, int idZanr, int idIzdavac, string naslov, DateTime datumObjavljivanja, string iSBN, int ukupanBrojKopija, int brojStranica, string jezik, string opis)
        {
            IdKnjiga = idKnjiga;
            IdZanr = idZanr;
            IdIzdavac = idIzdavac;
            Naslov = naslov;
            DatumObjavljivanja = datumObjavljivanja;
            ISBN = iSBN;
            UkupanBrojKopija = ukupanBrojKopija;
            BrojStranica = brojStranica;
            Jezik = jezik;
            Opis = opis;
        }

        public Knjiga()
        {
        }

        public override bool Equals(object obj)
        {
            return obj is Knjiga knjiga &&
                   IdKnjiga == knjiga.IdKnjiga;
        }



        public override string ToString()
        {
            return "KNJIGA: " + IdKnjiga + " " + ISBN + " " + Naslov + " " + DatumObjavljivanja;
        }

        public override int GetHashCode()
        {
            return 693581416 + IdKnjiga.GetHashCode();
        }
    }
}
