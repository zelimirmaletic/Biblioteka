using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Autor
    {
        public int IdAutor { get; set; }
        public int IdMjesto { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string DatumRodjenja { get; set; }

        public Autor()
        {
        }

        public Autor(int idAutor,int idMjesto, string ime, string prezime, string datumRodjenja)
        {
            IdAutor = idAutor;
            IdMjesto = idMjesto;
            Ime = ime;
            Prezime = prezime;
            DatumRodjenja = datumRodjenja;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Autor a = (Autor)obj;
                return IdAutor == a.IdAutor;
            }
        }

        public override int GetHashCode()
        {
            return -1644406393 + IdAutor.GetHashCode();
        }

        public override string ToString()
        {
            return "AUTOR: " + IdAutor + " " + Ime + " " + Prezime + " " + DatumRodjenja.ToString();
        }
    }
}
