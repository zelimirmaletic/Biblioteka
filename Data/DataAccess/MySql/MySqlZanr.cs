﻿using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlZanr
    {
        private static readonly string SELECT = "SELECT IdZanr, Naziv, Opis FROM `Zanr` ORDER BY Naziv";
        private static readonly string INSERT = "INSERT INTO `Zanr`(Naziv, Opis) VALUES (@Naziv,@Opis)";
        private static readonly string UPDATE = "UPDATE `Zanr` SET Naziv=@Naziv, Opis=@Opis WHERE IdZanr=@IdZanr";
        private static readonly string DELETE = "DELETE FROM `Zanr` WHERE IdZanr=@IdZanr";


        private void InsertZanr(Zanr zanr)
        {
            MySqlConnection connection = null;
            MySqlCommand cmd;
            try
            {
                connection = MySqlUtil.GetConnection();
                cmd = connection.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@Naziv", zanr.Naziv);
                cmd.Parameters.AddWithValue("@Opis", zanr.Opis);
                cmd.ExecuteNonQuery();
                zanr.IdZanr = (int)cmd.LastInsertedId;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlZanr", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(connection);
            }
        }

        private void UpdateZanr(Zanr zanr)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdZanr", zanr.IdZanr);
                cmd.Parameters.AddWithValue("@Naziv", zanr.Naziv);
                cmd.Parameters.AddWithValue("@Opis", zanr.Opis);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlZanr", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public void DeleteZanrById(int idZanr)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdZanr", idZanr);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlZanr", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Zanr> GetAllZanr()
        {
            List<Zanr> result = new List<Zanr>();
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
                    result.Add(new Zanr()
                    {
                        IdZanr = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Opis = reader.GetString(2)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlZanr", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }


        public Zanr GetZanrByID(int IdZanr)
        {
            var result = new Zanr();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Zanr` WHERE IdZanr=@IdZanr";
                cmd.Parameters.AddWithValue("@IdZanr", IdZanr);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Zanr()
                {
                    IdZanr = reader.GetInt32(0),
                    Naziv = reader.GetString(1),
                    Opis = reader.GetString(2)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlMjesto", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public int GetBrojZanrova()
        {
            var result = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Zanr`";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    result++;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlMjesto", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public void SaveZanr(Zanr zanr)
        {
            if (zanr.IdZanr <= 0)
            {
                InsertZanr(zanr);
            }
            else
            {
                UpdateZanr(zanr);
            }
        }

    }
}
