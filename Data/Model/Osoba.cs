﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.Model
{
    public class Osoba
    {
        public int IdOsoba { get; set; }
        public string NazivMjesta { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string Email { get; set; }

        public Osoba()
        {
        }
        public Osoba(int idOsoba, string nazivMjesta, string ime, string prezime, string adresa, string brojTelefona,string email, DateTime datumRodjenja)
        {
            IdOsoba = idOsoba;
            NazivMjesta = nazivMjesta;
            Ime = ime;
            Prezime = prezime;
            Adresa = adresa;
            BrojTelefona = brojTelefona;
            Email = email;
            DatumRodjenja = datumRodjenja;
        }

        public override bool Equals(object obj)
        {
            return obj is Osoba osoba &&
                   IdOsoba == osoba.IdOsoba;
        }

        public override int GetHashCode()
        {
            return 1850017762 + IdOsoba.GetHashCode();
        }

        public override string ToString()
        {
            return "OSOBA: "+ Ime + " " + Prezime + " " + Adresa + " " + BrojTelefona;
        }
    }
}
