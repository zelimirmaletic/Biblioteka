using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlIzdavac
    {
        private static readonly string SELECT = "SELECT * FROM `Izdavac` ORDER BY Naziv";
        private static readonly string INSERT = "INSERT INTO `Izdavac`(IdMjesto, Naziv, Adresa) VALUES (@IdMjesto, @Naziv, @Adresa)";
        private static readonly string UPDATE = "UPDATE `Izdavac` SET IdMjesto=@IdMjesto, Naziv=@Naziv, Adresa=@Adresa WHERE IdIzdavac=@IdIzdavac";
        private static readonly string DELETE = "DELETE FROM `Izdavac` WHERE IdIzdavac=@IdIzdavac";

        private void InsertIzdavac(Izdavac izdavac)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdMjesto", izdavac.IdMjesto);
                cmd.Parameters.AddWithValue("@Naziv", izdavac.Naziv);
                cmd.Parameters.AddWithValue("@Adresa", izdavac.Adresa);
                cmd.ExecuteNonQuery();
                izdavac.IdIzdavac = (int)cmd.LastInsertedId;
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlIzdavac", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateIzdavac(Izdavac izdavac)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdIzdavac", izdavac.IdIzdavac);
                cmd.Parameters.AddWithValue("@IdMjesto", izdavac.IdMjesto);
                cmd.Parameters.AddWithValue("@Naziv", izdavac.Naziv);
                cmd.Parameters.AddWithValue("@Adresa", izdavac.Adresa);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlIzdavac", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public void DeleteIzdavacById(int IdIzdavac)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdIzdavac", IdIzdavac);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlIzdavac", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Izdavac> GetAllIzdavac()
        {
            var result = new List<Izdavac>();
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
                    result.Add(new Izdavac()
                    {
                        IdIzdavac = reader.GetInt32(0),
                        IdMjesto = reader.GetInt32(1),
                        Naziv = reader.GetString(2),
                        Adresa = reader.GetString(3)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlIzdavac", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public Izdavac GetIzdavacByID(int IdIzdavac)
        {
            var result = new Izdavac();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Izdavac` WHERE IdIzdavac=@IdIzdavac";
                cmd.Parameters.AddWithValue("@IdAutor", IdIzdavac);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Izdavac()
                {
                    IdIzdavac = reader.GetInt32(0),
                    IdMjesto = reader.GetInt32(1),
                    Naziv = reader.GetString(2),
                    Adresa = reader.GetString(3)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlIzdavac", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }


        public int GetBrojIzdavaca()
        {
            var result = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Izdavac`";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    result++;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlIzdavac", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }



        public void SaveIzdavac(Izdavac izdavac)
        {
            if (izdavac.IdIzdavac <= 0)
            {
                InsertIzdavac(izdavac);
            }
            else
            {
                UpdateIzdavac(izdavac);
            }
        }
    }
}
