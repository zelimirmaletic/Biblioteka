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
    class MySqlTema
    {
        private static readonly string INSERT = "insert into `Tema` (IdOsoba, Stil) values (@IdOsoba, @Stil)";
        private static readonly string UPDATE = "update `Tema` SET Stil=@Stil WHERE IdOsoba=@IdOsoba";
        private static readonly string SELECT = "select * from `Tema` where IdOsoba = @IdOsoba;";

        public void SaveTema(Tema tema)
        {
            if (tema.IdTema <= 0)
            {
                InsertTema(tema);
            }
            else
            {
                UpdateTema(tema);
            }
        }

        private void InsertTema(Tema tema)
        {
            MySqlConnection connection = null;
            MySqlCommand cmd;
            try
            {
                connection = MySqlUtil.GetConnection();
                cmd = connection.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdOsoba", tema.IdOsoba);
                cmd.Parameters.AddWithValue("@Stil", tema.Stil);
                cmd.ExecuteNonQuery();
                tema.IdTema = (int)cmd.LastInsertedId;
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

        private void UpdateTema(Tema tema)
        {
            MySqlConnection connection = null;
            MySqlCommand cmd;
            try
            {
                connection = MySqlUtil.GetConnection();
                cmd = connection.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdOsoba", tema.IdOsoba);
                cmd.Parameters.AddWithValue("@Stil", tema.Stil);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlTema", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(connection);
            }

        }

        public Tema GetTemaByOsobaId(int id)
        {
            var tema = new Tema();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = SELECT;
                cmd.Parameters.AddWithValue("@IdOsoba", id);
                reader = cmd.ExecuteReader();
                reader.Read();
                tema.IdTema = reader.GetInt32(0);
                tema.IdOsoba = reader.GetInt32(1);
                tema.Stil = reader.GetInt32(2);
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlTema", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return tema;

        }
    }
}
