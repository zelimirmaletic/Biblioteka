using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Knjiga_ima_Autor
    {
        public int IdKnjiga { get; set; }
        public int IdAutor { get; set; }

        public Knjiga_ima_Autor()
        {
        }

        public Knjiga_ima_Autor(int idKnjiga, int idAutor)
        {
            IdKnjiga = idKnjiga;
            IdAutor = idAutor;
        }

        public override bool Equals(object obj)
        {
            return obj is Knjiga_ima_Autor autor &&
                   IdKnjiga == autor.IdKnjiga &&
                   IdAutor == autor.IdAutor;
        }

        public override int GetHashCode()
        {
            int hashCode = -1883040361;
            hashCode = hashCode * -1521134295 + IdKnjiga.GetHashCode();
            hashCode = hashCode * -1521134295 + IdAutor.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "KNJIGA_ima_AUTOR: " + IdKnjiga + " " + IdAutor;
        }
    }
}
