using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlClan
    {
        private static readonly string SELECT = "SELECT * FROM `Clan` ";

        private static readonly string INSERT = "INSERT INTO `Clan`(IdClan, DatumUclanjivanja, DatumObnavljanjaClanstva) " +
                                                            "VALUES (@IdClan, @DatumUclanjivanja, @DatumObnavljanjaClanstva)";

        private static readonly string UPDATE = "UPDATE `Clan` SET DatumObnavljanjaClanstva=@DatumObnavljanjaClanstva WHERE IdClan=@IdClan";

        private static readonly string DELETE = "DELETE FROM `Clan` WHERE IdClan=@IdClan";

        private void InsertClan(Clan clan)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdClan", clan.IdClan);
                cmd.Parameters.AddWithValue("DatumUclanjivanja", clan.DatumUclanjivanja);
                cmd.Parameters.AddWithValue("@DatumObnavljanjaClanstva", clan.DatumObnavljanjaClanstva);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlClan", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateClan(Clan clan)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@DatumObnavljanjaClanstva", clan.DatumObnavljanjaClanstva);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlClan", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public void DeleteClanById(int IdClan)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdOsoba", IdClan);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlClan", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Clan> GetAllClan()
        {
            var result = new List<Clan>();
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
                    result.Add(new Clan()
                    {
                        IdClan = reader.GetInt32(0),
                        DatumUclanjivanja = reader.GetDateTime(1),
                        DatumObnavljanjaClanstva = reader.GetDateTime(2)
                        
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlClan", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public Clan GetClanByID(int IdClan)
        {
            var result = new Clan();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Clan` WHERE IdClan=@IdClan";
                cmd.Parameters.AddWithValue("@IdClan", IdClan);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Clan()
                {
                    IdClan= reader.GetInt32(0),
                    DatumUclanjivanja = reader.GetDateTime(1),
                    DatumObnavljanjaClanstva = reader.GetDateTime(2)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlClan", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public DataTable GetClanOsobaJoin(string ime, string prezime)
        {
            var table = new DataTable();
            //Define columns
            var IdClan = new DataColumn("Šifra", typeof(int));
            var DatumUclanjivanja = new DataColumn("Datum učlanjivanja", typeof(string));
            var DatumObnavljanjaČlanstva = new DataColumn("Datum obnavljanja članstva", typeof(string));
            var Ime = new DataColumn("Ime", typeof(string));
            var Prezime = new DataColumn("Prezime", typeof(string));
            var DatumRodjenja = new DataColumn("Datum rođenja", typeof(string));
            var NazivMjesta = new DataColumn("Mjesto", typeof(string));
            var Adresa = new DataColumn("Adresa", typeof(string));
            var BrojTelefona = new DataColumn("Telefon", typeof(string));
            var Email = new DataColumn("Email", typeof(string));
            //Add columns to a table
            table.Columns.Add(IdClan);
            table.Columns.Add(DatumUclanjivanja);
            table.Columns.Add(DatumObnavljanjaČlanstva);
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
                string query = "select Clan.IdClan, Clan.DatumUclanjivanja, Clan.DatumObnavljanjaClanstva, Osoba.Ime, Osoba.Prezime,Osoba.DatumRodjenja, Osoba.NazivMjesta, Osoba.Adresa, Osoba.BrojTelefona, Osoba.Email from Clan inner join Osoba on Osoba.IdOsoba=Clan.IdClan " +
                    " WHERE Ime LIKE @Ime AND Prezime LIKE @Prezime ;";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Ime", ime + "%");
                cmd.Parameters.AddWithValue("@Prezime", prezime + "%");
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var row = table.NewRow();
                    row[0] = reader.GetInt32(0);
                    row[1] = reader.GetDateTime(1).ToString("dd/MM/yyyy");
                    row[2] = reader.GetDateTime(2).ToString("dd/MM/yyyy");
                    row[3] = reader.GetString(3);
                    row[4] = reader.GetString(4);
                    row[5] = reader.GetDateTime(5).ToString("dd/MM/yyyy");
                    row[6] = reader.GetString(6);
                    row[7] = reader.GetString(7);
                    row[8] = reader.GetString(8);
                    row[9] = reader.GetString(9);
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

        public void UpdateClanstvo(int id)
        {
            
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "update Clan set DatumObnavljanjaClanstva=DATE_ADD(CURDATE(), INTERVAL 1 YEAR) where IdClan=@IdClan;";
                cmd.Parameters.AddWithValue("@IdClan",id);
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlClan", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
        }



        public int GetBrojClanova()
        {
            var count = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `Clan`";
                reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlClan", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return count;
        }

        public void SaveClan(Clan clan, string action)
        {
            if (action.Equals("insert"))
            {
                InsertClan(clan);
            }
            else if(action.Equals("update"))
            {
                UpdateClan(clan);
            }
        }
    }
}
