using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlOsoba
    {
        private static readonly string SELECT = "SELECT * FROM `Osoba` ORDER BY Prezime";

        private static readonly string INSERT = "INSERT INTO `Osoba`(NazivMjesta, Ime, Prezime, Adresa, BrojTelefona, Email, DatumRodjenja) " +
                                                            "VALUES (@NazivMjesta, @Ime, @Prezime, @Adresa, @BrojTelefona, @Email, @DatumRodjenja)";

        private static readonly string UPDATE = "UPDATE `Osoba` SET NazivMjesta=@NazivMjesta, Ime=@Ime, Prezime=@Prezime, Adresa=@Adresa, BrojTelefona=@BrojTelefona, Email=@Email, DatumRodjenja=@DatumRodjenja WHERE IdOsoba=@IdOsoba";

        private static readonly string DELETE = "DELETE FROM `Osoba` WHERE IdOsoba=@IdOsoba";

        private void InsertOsoba(Osoba osoba)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdOsoba", osoba.IdOsoba);
                cmd.Parameters.AddWithValue("@NazivMjesta", osoba.NazivMjesta);
                cmd.Parameters.AddWithValue("@Ime", osoba.Ime);
                cmd.Parameters.AddWithValue("@Prezime", osoba.Prezime);
                cmd.Parameters.AddWithValue("@Adresa", osoba.Adresa);
                cmd.Parameters.AddWithValue("@BrojTelefona", osoba.BrojTelefona);
                cmd.Parameters.AddWithValue("@Email", osoba.Email);
                cmd.Parameters.AddWithValue("@DatumRodjenja", osoba.DatumRodjenja);
                cmd.ExecuteNonQuery();
                osoba.IdOsoba = (int)cmd.LastInsertedId;
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlOsoba", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateOsoba(Osoba osoba)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdOsoba", osoba.IdOsoba);
                cmd.Parameters.AddWithValue("@NazivMjesta", osoba.NazivMjesta);
                cmd.Parameters.AddWithValue("@Ime", osoba.Ime);
                cmd.Parameters.AddWithValue("@Prezime", osoba.Prezime);
                cmd.Parameters.AddWithValue("@Adresa", osoba.Adresa);
                cmd.Parameters.AddWithValue("@BrojTelefona", osoba.BrojTelefona);
                cmd.Parameters.AddWithValue("@Email", osoba.Email);
                cmd.Parameters.AddWithValue("@DatumRodjenja", osoba.DatumRodjenja);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlOsoba Update", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public void DeleteOsobaById(int IdOsoba)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdOsoba", IdOsoba);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlOsoba", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Osoba> GetAllOsoba()
        {
            var result = new List<Osoba>();
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
                    result.Add(new Osoba()
                    {
                        IdOsoba = reader.GetInt32(0),
                        NazivMjesta = reader.GetString(1),
                        Ime = reader.GetString(2),
                        Prezime = reader.GetString(3),
                        Adresa = reader.GetString(4),
                        BrojTelefona = reader.GetString(5),
                        Email = reader.GetString(6),
                        DatumRodjenja = reader.GetDateTime(7)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlOsoba", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public Osoba GetOsobaByID(int IdOsoba)
        {
            var result = new Osoba();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Osoba` WHERE IdOsoba=@IdOsoba";
                cmd.Parameters.AddWithValue("@IdOsoba", IdOsoba);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Osoba()
                {
                    IdOsoba = reader.GetInt32(0),
                    NazivMjesta = reader.GetString(1),
                    Ime = reader.GetString(2),
                    Prezime = reader.GetString(3),
                    Adresa = reader.GetString(4),
                    BrojTelefona = reader.GetString(5),
                    Email = reader.GetString(6),
                    DatumRodjenja = reader.GetDateTime(7)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlOsoba", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public void SaveOsoba(Osoba osoba)
        {
            if (osoba.IdOsoba <= 0)
            {
                InsertOsoba(osoba);
            }
            else
            {
                UpdateOsoba(osoba);
            }
        }
    }
}
