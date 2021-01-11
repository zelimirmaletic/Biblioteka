using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlMjesto
    {
        private static readonly string SELECT = "SELECT IdMjesto, Naziv, PostanskiBroj FROM `Mjesto` ORDER BY Naziv";
        private static readonly string INSERT = "INSERT INTO `Mjesto` (Naziv, PostanskiBroj) VALUES (@Naziv, @PostanskiBroj)";
        private static readonly string UPDATE = "UPDATE `Mjesto` SET Naziv=@Naziv, PostanskiBroj=@PostanskiBroj WHERE IdMjesto=@IdMjesto";
        private static readonly string DELETE = "DELETE FROM `Mjesto` WHERE IdMjesto=@IdMjesto";

        private void InsertMjesto(Mjesto mjesto)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@Naziv", mjesto.Naziv);
                cmd.Parameters.AddWithValue("@PostanskiBroj", mjesto.PostanskiBroj);
                cmd.ExecuteNonQuery();
                mjesto.IdMjesto = (int)cmd.LastInsertedId;
            } catch(Exception exc)
            {
                throw new DataAccessException("Exception in MySqlMjesto",exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateMjesto(Mjesto mjesto)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdMjesto", mjesto.IdMjesto);
                cmd.Parameters.AddWithValue("@Naziv", mjesto.Naziv);
                cmd.Parameters.AddWithValue("@PostanskiBroj", mjesto.PostanskiBroj);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlMjesto",exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void DeleteMjestoById(int IdMjesto)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdMjesto", IdMjesto);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlMjesto", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Mjesto> GetAllMjesto()
        {
            var result = new List<Mjesto>();
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
                    result.Add(new Mjesto()
                    {
                        IdMjesto = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        PostanskiBroj = reader.GetString(2)
                    });
                }
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

        public Mjesto GetMjestoByID(int IdMjesto)
        {
            var result = new Mjesto();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT IdMjesto, Naziv, PostanskiBroj FROM `Mjesto` WHERE IdMjesto=@IdMjesto";
                cmd.Parameters.AddWithValue("@IdMjesto", IdMjesto);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Mjesto()
                {
                    IdMjesto = reader.GetInt32(0),
                    Naziv = reader.GetString(1),
                    PostanskiBroj = reader.GetString(2)
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

        public void SaveMjesto(Mjesto mjesto)
        {
            if (mjesto.IdMjesto <= 0)
            {
                InsertMjesto(mjesto);
            }
            else
            {
                UpdateMjesto(mjesto);
            }
        }
    }
}
