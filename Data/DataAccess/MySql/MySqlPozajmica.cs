using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlPozajmica
    {
        private static readonly string SELECT = "SELECT * FROM `Pozajmica`";
        private static readonly string INSERT = "INSERT INTO `Pozajmica`(IdClan, IdKnjiga, IdBibliotekar, DatumPozajmljivanja, JeRazduzena, Opis) " +
                                                               "VALUES (@IdClan, @IdKnjiga, @IdBibliotekar, @DatumPozajmljivanja, @JeRazduzena,@Opis)";

        private static readonly string DELETE = "DELETE FROM `Pozajmica` WHERE IdPozajmica=@IdPozajmica";

        private static readonly int DUZINA_POZAJMICE = 30;
        private static readonly float CIJENA_PO_DANU = 0.1F;
        private static readonly int PRODUZAVANJE = 30;

        private void InsertPozajmica(Pozajmica  pozajmica)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdClan", pozajmica.IdClan);
                cmd.Parameters.AddWithValue("@IdKnjiga", pozajmica.IdKnjiga);
                cmd.Parameters.AddWithValue("@IdBibliotekar", pozajmica.IdBibliotekar);
                cmd.Parameters.AddWithValue("@DatumPozajmljivanja",pozajmica.DatumPozajmljivanja);
                cmd.Parameters.AddWithValue("@JeRazduzena", pozajmica.JeRazduzena);
                cmd.Parameters.AddWithValue("@Opis", pozajmica.Opis);
                cmd.ExecuteNonQuery();
                pozajmica.IdPozajmica = (int)cmd.LastInsertedId;
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public int GetUkupanBrojPozajmica()
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            int count = 0;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `Pozajmica` WHERE JeRazduzena=false;";
                reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);      
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return count;
        }

        public int GetUkupanBrojPozajmicaByKnjigaId(int IdKnjiga)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            int count = 0;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `Pozajmica` WHERE IdKnjiga=@IdKnjiga AND JeRazduzena=0;";
                cmd.Parameters.AddWithValue("@IdKnjiga", IdKnjiga);
                reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return count;
        }

        public int GetUkupanBrojKasnihPozajmica()
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            int count = 0;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `Pozajmica` WHERE DATE_ADD(DatumPozajmljivanja, INTERVAL @NumberOfDays DAY) < CURDATE() AND JeRazduzena=false;";
                cmd.Parameters.AddWithValue("@NumberOfDays", DUZINA_POZAJMICE);
                reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);

            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return count;
        }

        public void PozajmicaProduzi(int id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            int count = 0;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Pozajmica SET DatumPozajmljivanja=DATE_ADD(DatumPozajmljivanja, INTERVAL @NumberOfDays DAY) WHERE IdPozajmica=@IdPozajmica";
                cmd.Parameters.AddWithValue("@NumberOfDays", PRODUZAVANJE);
                cmd.Parameters.AddWithValue("@IdPozajmica", id);
                reader = cmd.ExecuteReader();
                reader.Read();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
        }

        public void PozajmicaRazduzi(int id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            int count = 0;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Pozajmica SET JeRazduzena=1 WHERE IdPozajmica=@IdPozajmica";
                cmd.Parameters.AddWithValue("@IdPozajmica", id);
                reader = cmd.ExecuteReader();
                reader.Read();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
        }


        public Pozajmica GetPozajmicaByClanID(int IdClan)
        {
            var result = new Pozajmica();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Pozajmica` WHERE IdClan=@IdClan";
                cmd.Parameters.AddWithValue("@IdClan", IdClan);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Pozajmica()
                {
                    IdPozajmica = reader.GetInt32(0),
                    IdClan = reader.GetInt32(1),
                    IdBibliotekar = reader.GetInt32(2),
                    DatumPozajmljivanja = reader.GetDateTime(3),
                    JeRazduzena = reader.GetBoolean(4),
                    Opis = reader.GetString(5)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }


        public DataTable GetPozajmicaJoin(int id)
        {
            var table = new DataTable();
            //Define columns
            var Sifra = new DataColumn("Šifra", typeof(int));
            var Naslov = new DataColumn("Naslov", typeof(string));
            var Ime = new DataColumn("Ime", typeof(string));
            var Prezime = new DataColumn("Prezime", typeof(string));
            var Bibliotekar = new DataColumn("Bibliotekar", typeof(string));
            var DatumPozajmljivanja = new DataColumn("Datum pozajmljivanja", typeof(string));
            var Rok = new DataColumn("Rok za vraćanje", typeof(string));
            var Opis = new DataColumn("Opis", typeof(string));
            //Add columns to a table
            table.Columns.Add(Sifra);
            table.Columns.Add(Naslov);
            table.Columns.Add(Ime);
            table.Columns.Add(Prezime);
            table.Columns.Add(Bibliotekar);
            table.Columns.Add(DatumPozajmljivanja);
            table.Columns.Add(Rok);
            table.Columns.Add(Opis);

            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                string query = "SELECT IdPozajmica, Knjiga.Naslov, Osoba.Ime, Osoba.Prezime, Bibliotekar.KorisnickoIme AS Bibliotekar, DatumPozajmljivanja, Pozajmica.Opis " +
                                "FROM Pozajmica INNER JOIN Osoba ON Osoba.IdOsoba=Pozajmica.IdClan INNER JOIN Bibliotekar ON Bibliotekar.IdBibliotekar=Pozajmica.IdBibliotekar " +
                                "INNER JOIN Knjiga ON Knjiga.IdKnjiga=Pozajmica.IdKnjiga WHERE Pozajmica.JeRazduzena=false AND IdClan=@IdClan";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@IdClan",id);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var row = table.NewRow();
                    row[0] = reader.GetInt32(0);
                    row[1] = reader.GetString(1);
                    row[2] = reader.GetString(2);
                    row[3] = reader.GetString(3);
                    row[4] = reader.GetString(4);
                    row[5] = reader.GetDateTime(5).ToString("dd/MM/yyyy");
                    if (reader.GetDateTime(5).CompareTo(DateTime.Today.AddDays(DUZINA_POZAJMICE)) > 1)
                        row[6] = "POZAJMICA JE ISTEKLA!";
                    else
                        row[6] = reader.GetDateTime(5).AddDays((double)DUZINA_POZAJMICE).ToString("dd/MM/yyyy");
                    try
                    {
                        row[7] = reader.GetString(6);
                    }
                    catch(Exception exc)
                    {
                        row[7] = "Opis nije dodan";
                    }
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

        public float GetPozajmicaCijena(int id)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;
            float sum = 0;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT SUM(DATEDIFF(CURDATE(),DATE_ADD(DatumPozajmljivanja, INTERVAL 30 DAY)))*@CijenaPoDanu AS Iznos FROM `Pozajmica` WHERE (DATE_ADD(DatumPozajmljivanja, INTERVAL 30 DAY) < CURDATE()) AND JeRazduzena=false;";
                cmd.Parameters.AddWithValue("@NumberOfDays", DUZINA_POZAJMICE);
                cmd.Parameters.AddWithValue("@CijenaPoDanu", CIJENA_PO_DANU);
                reader = cmd.ExecuteReader();
                reader.Read();
                try
                {
                    sum = reader.GetFloat(0);
                }
                catch (Exception exc)
                {
                    sum = 0;
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return sum;
        }

        public void SavePozajmica(Pozajmica pozajmica)
        {
            if (pozajmica.IdPozajmica <= 0)
            {
                InsertPozajmica(pozajmica);
            }
            else
            {
                //UpdatePozajmica(pozajmica);
            }
        }
    }
}
