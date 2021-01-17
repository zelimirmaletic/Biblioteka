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
    class MySqlZanr
    {
        private static readonly string SELECT = "SELECT Naziv, Opis FROM `Zanr` ORDER BY Naziv";
        private static readonly string INSERT = "INSERT INTO `Zanr`(Naziv, Opis) VALUES (@Naziv,@Opis)";
        private static readonly string UPDATE = "UPDATE `Zanr` SET Naziv=@Naziv, Opis=@Opis WHERE Naziv=@Naziv";
        private static readonly string DELETE = "DELETE FROM `Zanr` WHERE Naziv=@Naziv";


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

        public void DeleteZanrById(string naziv)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@Naziv", naziv);
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
                        Naziv = reader.GetString(0),
                        Opis = reader.GetString(1)
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

        public int GetBrojZanrova()
        {
            var count = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `Zanr`";
                reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlMjesto", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return count;
        }

        public void SaveZanr(Zanr zanr)
        {

        }

    }
}
