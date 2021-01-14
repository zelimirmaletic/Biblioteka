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
    class MySqlAutor
    {
        private static readonly string SELECT = "SELECT * FROM `Autor` ORDER BY Prezime";
        private static readonly string INSERT = "INSERT INTO `Autor`(IdMjesto, Ime, Prezime, DatumRodjenja) VALUES (@IdMjesto, @Ime, @Prezime, @DatumRodjenja)";
        private static readonly string UPDATE = "UPDATE `Autor` SET IdMjesto=@IdMjesto, Ime=@Ime, Prezime=@Prezime, DatumRodjenja=@DatumRodjenja WHERE IdAutor=@IdAutor";
        private static readonly string DELETE = "DELETE FROM `Autor` WHERE IdAutor=@IdAutor";

        private void InsertAutor(Autor autor)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdMjesto", autor.IdMjesto);
                cmd.Parameters.AddWithValue("@Ime", autor.Ime);
                cmd.Parameters.AddWithValue("@Prezime", autor.Prezime);
                cmd.Parameters.AddWithValue("@DatumRodjenja", autor.DatumRodjenja);
                cmd.ExecuteNonQuery();
                autor.IdAutor = (int)cmd.LastInsertedId;
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlAutor", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateAutor(Autor autor)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdAutor", autor.IdAutor);
                cmd.Parameters.AddWithValue("@IdMjesto", autor.IdMjesto);
                cmd.Parameters.AddWithValue("@Ime", autor.Ime);
                cmd.Parameters.AddWithValue("@Prezime", autor.Prezime);
                cmd.Parameters.AddWithValue("@DatumRodjenja", autor.DatumRodjenja);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlAutor", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public void DeleteAutorById(int IdAutor)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdAutor", IdAutor);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlAutor", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Autor> GetAllAutor()
        {
            var result = new List<Autor>();
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
                    result.Add(new Autor()
                    {
                       IdAutor = reader.GetInt32(0),
                       IdMjesto = reader.GetInt32(1),
                       Ime = reader.GetString(2),
                       Prezime = reader.GetString(3),
                       DatumRodjenja = reader.GetDateTime(4)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlAutor", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public Autor GetAutorByID(int IdAutor)
        {
            var result = new Autor();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Autor` WHERE IdAutor=@IdAutor";
                cmd.Parameters.AddWithValue("@IdAutor", IdAutor);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Autor()
                {
                    IdAutor = reader.GetInt32(0),
                    IdMjesto = reader.GetInt32(1),
                    Ime = reader.GetString(2),
                    Prezime = reader.GetString(3),
                    DatumRodjenja = reader.GetDateTime(4)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlAutor", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public int GetBrojAutora()
        {
            var result = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Autor`";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                    result++;
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlAutor", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public void SaveAutor(Autor autor)
        {
            if (autor.IdAutor <= 0)
            {
                InsertAutor(autor);
            }
            else
            {
                UpdateAutor(autor);
            }
        }

    }
}
