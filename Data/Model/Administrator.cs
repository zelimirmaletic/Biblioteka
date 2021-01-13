using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    class Administrator
    {
        public int IdAdministrator { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }

        public Administrator()
        {
        }

        public Administrator(int idAdministrator, string korisnickoIme, string lozinka)
        {
            IdAdministrator = idAdministrator;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
        }

        public override bool Equals(object obj)
        {
            return obj is Administrator administrator &&
                   IdAdministrator == administrator.IdAdministrator &&
                   KorisnickoIme == administrator.KorisnickoIme;
        }

        public override int GetHashCode()
        {
            int hashCode = 1863767116;
            hashCode = hashCode * -1521134295 + IdAdministrator.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(KorisnickoIme);
            return hashCode;
        }

        public override string ToString()
        {
            return "ADMINISTRATOR: " + IdAdministrator;
        }
    }
}
