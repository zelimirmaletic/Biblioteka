using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlBibliotekar
    {
        private static readonly string SELECT = "SELECT * FROM `Bibliotekar` ";
        private static readonly string INSERT = "INSERT INTO `Bibliotekar`(IdBibliotekar, KorisnickoIme, Lozinka) VALUES (@IdBibliotekar, @KorisnickoIme, @Lozinka)";
        private static readonly string UPDATE = "UPDATE `Bibliotekar` SET KorisnickoIme=@KorisnickoIme, Lozinka=@Lozinka WHERE IdBibliotekar=@IdBibliotekar";
        private static readonly string DELETE = "DELETE FROM `Bibliotekar` WHERE IdBibliotekar=@IdBibliotekar";

        private void InsertBibliotekar(Bibliotekar bibliotekar)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdBibliotekar", bibliotekar.IdBibliotekar);
                cmd.Parameters.AddWithValue("@KorisnickoIme", bibliotekar.KorisnickoIme);
                cmd.Parameters.AddWithValue("@Lozinka", bibliotekar.Lozinka);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlBibliotekar", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateBibliotekar(Bibliotekar bibliotekar)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdBibliotekar", bibliotekar.IdBibliotekar);
                cmd.Parameters.AddWithValue("@KorisnickoIme", bibliotekar.KorisnickoIme);
                cmd.Parameters.AddWithValue("@Lozinka", bibliotekar.Lozinka);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlBibliotekar", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public void DeleteBibliotekarById(int IdBibliotekar)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdBibliotekar", IdBibliotekar);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlBibliotekar", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public DataTable GetBibliotekarOsobaJoin(string ime, string prezime)
        {
            var table = new DataTable();
            //Define columns
            var IdClan = new DataColumn("Šifra", typeof(int));
            var KorisnickoIme = new DataColumn("Korisničko ime", typeof(string));
            var Ime = new DataColumn("Ime", typeof(string));
            var Prezime = new DataColumn("Prezime", typeof(string));
            var DatumRodjenja = new DataColumn("Datum rođenja", typeof(string));
            var NazivMjesta = new DataColumn("Mjesto", typeof(string));
            var Adresa = new DataColumn("Adresa", typeof(string));
            var BrojTelefona = new DataColumn("Telefon", typeof(string));
            var Email = new DataColumn("Email", typeof(string));
            //Add columns to a table
            table.Columns.Add(IdClan);
            table.Columns.Add(KorisnickoIme);
            table.Columns.Add(Ime);
            table.Columns.Add(Prezime);
            table.Columns.Add(DatumRodjenja);
            table.Columns.Add(NazivMjesta);
            table.Columns.Add(Adresa);
            table.Columns.Add(BrojTelefona);
            table.Columns.Add(Email);

            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                string query = "select Bibliotekar.IdBibliotekar, Bibliotekar.KorisnickoIme, Osoba.Ime, Osoba.Prezime,Osoba.DatumRodjenja, Osoba.NazivMjesta, Osoba.Adresa, Osoba.BrojTelefona, Osoba.Email from Bibliotekar inner join Osoba on Osoba.IdOsoba=Bibliotekar.IdBibliotekar " +
                    " WHERE Ime LIKE @Ime AND Prezime LIKE @Prezime ;";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Ime", ime + "%");
                cmd.Parameters.AddWithValue("@Prezime", prezime + "%");
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetInt16(0) == 1)
                        continue;
                    var row = table.NewRow();
                    row[0] = reader.GetInt32(0);
                    row[1] = reader.GetString(1);
                    row[2] = reader.GetString(2);
                    row[3] = reader.GetString(3);
                    row[4] = reader.GetDateTime(4).ToString("dd/MM/yyyy");
                    row[5] = reader.GetString(5);
                    row[6] = reader.GetString(6);
                    row[7] = reader.GetString(7);
                    row[8] = reader.GetString(8);
                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return table;
        }



        public List<Bibliotekar> GetAllBibliotekar()
        {
            var result = new List<Bibliotekar>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Bibliotekar()
                    {
                        IdBibliotekar = reader.GetInt32(0),
                        KorisnickoIme = reader.GetString(1),
                        Lozinka = reader.GetString(2)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlBibliotekar", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public Bibliotekar GetBibliotekarByUsername(string korisnickoIme)
        {
            var result = new Bibliotekar();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Bibliotekar` WHERE KorisnickoIme=@KorisnickoIme";
                cmd.Parameters.AddWithValue("@KorisnickoIme", korisnickoIme);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Bibliotekar()
                {
                    IdBibliotekar = reader.GetInt32(0),
                    KorisnickoIme = reader.GetString(1),
                    Lozinka = reader.GetString(2)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlBibliotekar", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public Bibliotekar GetBibliotekarById(int id)
        {
            var result = new Bibliotekar();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Bibliotekar` WHERE IdBibliotekar=@IdBibliotekar";
                cmd.Parameters.AddWithValue("@IdBibliotekar", id);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Bibliotekar()
                {
                    IdBibliotekar = reader.GetInt32(0),
                    KorisnickoIme = reader.GetString(1),
                    Lozinka = reader.GetString(2)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlBibliotekar", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }


        public void SaveBibliotekar(Bibliotekar bibliotekar, string action)
        {
            if (action.Equals("insert"))
            {
                InsertBibliotekar(bibliotekar);
            }
            else if (action.Equals("update"))
            {
                UpdateBibliotekar(bibliotekar);
            }
        }
    }
}
