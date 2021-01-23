using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlObavjestenje
    {
        private static readonly string INSERT = "insert into `Obavjestenje` (IdAdministrator,IdBibliotekar, Naslov,Datum,Tekst,ZaSve) values " +
                                                "(@IdAdmin, @IdBibliotekar, @Naslov, @Datum, @Tekst, @ZaSve) ;";
        private static readonly string JOIN = "select Osoba.Ime, Osoba.Prezime, Obavjestenje.IdBibliotekar, Obavjestenje.Naslov, Obavjestenje.Datum, Obavjestenje.Tekst, Obavjestenje.ZaSve " +
                                               "from Obavjestenje inner join Osoba on Osoba.IdOsoba=Obavjestenje.IdAdministrator where IdBibliotekar=@IdBibliotekar OR ZaSve=1;";
        private static readonly string JOIN_ALL = "select Osoba.Ime, Osoba.Prezime, Obavjestenje.IdBibliotekar, Obavjestenje.Naslov, Obavjestenje.Datum, Obavjestenje.Tekst, Obavjestenje.ZaSve " +
                                       "from Obavjestenje inner join Osoba on Osoba.IdOsoba=Obavjestenje.IdAdministrator;";

        public void Insert(Obavjestenje obavjestenje)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdAdmin", obavjestenje.IdAdministrator);
                cmd.Parameters.AddWithValue("@IdBibliotekar", obavjestenje.IdBibliotekar);
                cmd.Parameters.AddWithValue("@Naslov", obavjestenje.Naslov);
                cmd.Parameters.AddWithValue("@Datum", obavjestenje.Datum);
                cmd.Parameters.AddWithValue("@Tekst", obavjestenje.Tekst);
                cmd.Parameters.AddWithValue("@ZaSve", obavjestenje.ZaSve);

                cmd.ExecuteNonQuery();
                obavjestenje.IdObavjestenje = (int)cmd.LastInsertedId;
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }

        }

        public DataTable GetObavjestenjeJoin(int idBibliotekar)
        {
            var table = new DataTable();
            //Define columns
            var Ime = new DataColumn("Ime", typeof(string));
            var Prezime = new DataColumn("Prezime", typeof(string));
            var IdBibliotekar = new DataColumn("IdBibliotekar", typeof(int));
            var Naslov = new DataColumn("Naslov", typeof(string));
            var Datum = new DataColumn("Datum", typeof(string));
            var Tekst = new DataColumn("Tekst", typeof(string));
            var ZaSve = new DataColumn("ZaSve", typeof(int));
            //Add columns to a table
            table.Columns.Add(Ime);
            table.Columns.Add(Prezime);
            table.Columns.Add(IdBibliotekar);
            table.Columns.Add(Naslov);
            table.Columns.Add(Datum);
            table.Columns.Add(Tekst);
            table.Columns.Add(ZaSve);


            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = JOIN;
                cmd.Parameters.AddWithValue("@IdBibliotekar", idBibliotekar);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var row = table.NewRow();
                    row[0] = reader.GetString(0);
                    row[1] = reader.GetString(1);
                    row[2] = reader.GetInt32(2);
                    row[3] = reader.GetString(3);
                    row[4] = reader.GetDateTime(4).ToShortDateString();
                    row[5] = reader.GetString(5);
                    row[6] = reader.GetInt32(6);
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

        public DataTable GetAllObavjestenjeJoin()
        {
            var table = new DataTable();
            //Define columns
            var Ime = new DataColumn("Ime", typeof(string));
            var Prezime = new DataColumn("Prezime", typeof(string));
            var IdBibliotekar = new DataColumn("IdBibliotekar", typeof(int));
            var Naslov = new DataColumn("Naslov", typeof(string));
            var Datum = new DataColumn("Datum", typeof(string));
            var Tekst = new DataColumn("Tekst", typeof(string));
            var ZaSve = new DataColumn("ZaSve", typeof(int));
            //Add columns to a table
            table.Columns.Add(Ime);
            table.Columns.Add(Prezime);
            table.Columns.Add(IdBibliotekar);
            table.Columns.Add(Naslov);
            table.Columns.Add(Datum);
            table.Columns.Add(Tekst);
            table.Columns.Add(ZaSve);


            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = JOIN_ALL;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var row = table.NewRow();
                    row[0] = reader.GetString(0);
                    row[1] = reader.GetString(1);
                    row[2] = reader.GetInt32(2);
                    row[3] = reader.GetString(3);
                    row[4] = reader.GetDateTime(4).ToShortDateString();
                    row[5] = reader.GetString(5);
                    row[6] = reader.GetInt32(6);
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


    }
}
