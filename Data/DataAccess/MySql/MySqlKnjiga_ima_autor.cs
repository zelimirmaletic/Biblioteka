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
    class MySqlKnjiga_ima_autor
    {
        private static readonly string SELECT = "SELECT * FROM `Knjiga_ima_Autor` WHERE IdKnjiga=@IdKnjiga ORDER BY Naziv";
        private static readonly string INSERT = "INSERT INTO `Knjiga_ima_Autor`(IdKnjiga, IdAutor) VALUES (@IdKnjiga, @Autor)";
        private static readonly string UPDATE = "UPDATE `Knjiga_ima_Autor` SET IdKnjiga=@IdKnjiga, IdAutor=@IdAutor WHERE IdKnjiga=@IdKnjiga";
        private static readonly string DELETE = "DELETE FROM `Knjiga_ima_Autor` WHERE IdKnjiga=@IdKnjiga";

        private void InsertKnjiga_ima_Autor(Knjiga_ima_Autor rekord)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdKnjiga", rekord.IdKnjiga);
                cmd.Parameters.AddWithValue("@IdAutor", rekord.IdAutor);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlKnjiga_ima_Autor", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateKnjiga_ima_Autor(Knjiga_ima_Autor rekord)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdKnjiga", rekord.IdKnjiga);
                cmd.Parameters.AddWithValue("@IdAutor", rekord.IdAutor);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlKnjiga_ima_Autor", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public void DeleteKnjiga_ima_AutorById(int IdKnjiga)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdKnjiga", IdKnjiga);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlKnjiga_ima_Autor", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Knjiga_ima_Autor> GetAllKnjiga_ima_Autor(int IdKnjiga)
        {
            var result = new List<Knjiga_ima_Autor>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT;
                cmd.Parameters.AddWithValue("@IdKnjiga", IdKnjiga);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Knjiga_ima_Autor()
                    {
                        IdKnjiga = reader.GetInt32(0),
                        IdAutor = reader.GetInt32(1)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlKnjiga_ima_Autor", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public void SaveKnjiga_ima_Izdavac(Knjiga_ima_Autor rekord, string mod)
        {
            if (mod == "insert")
            {
                InsertKnjiga_ima_Autor(rekord);
            }
            else
            {
                UpdateKnjiga_ima_Autor(rekord);
            }
        }
    }
}
