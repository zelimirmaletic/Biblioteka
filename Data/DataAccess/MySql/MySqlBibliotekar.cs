﻿using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
                        Lozinka = reader.GetString(3),
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
