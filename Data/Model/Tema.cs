using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Tema
    {
        public int IdTema { get; set; }
        public int IdOsoba { get; set; }
        public int Boja { get; set; }
        public string Font { get; set; }

        public Tema()
        {
        }

        public Tema(int idTema, int idOsoba, int boja, string font)
        {
            IdTema = idTema;
            IdOsoba = idOsoba;
            Boja = boja;
            Font = font;
        }

        public override bool Equals(object obj)
        {
            return obj is Tema tema &&
                   IdTema == tema.IdTema;
        }

        public override int GetHashCode()
        {
            return 1423958401 + IdTema.GetHashCode();
        }
    }
}
