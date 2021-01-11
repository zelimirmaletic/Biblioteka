using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;


namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlPozajmica
    {
        private static readonly string SELECT = "SELECT * FROM `Pozajmica`";
        private static readonly string INSERT = "INSERT INTO `Pozajmica`(IdClan, IdKnjiga, IdBibliotekar, DatumPozajmljivanja, JeRazduzena) " +
                                                               "VALUES (@IdClan, @IdKnjiga, @IdBibliotekar, @DatumPozajmljivanja, @JeRazduzena)";

        private static readonly string DELETE = "DELETE FROM `Pozajmica` WHERE IdPozajmica=@IdPozajmica";

        private void InsertPozajmica(Pozajmica  pozajmica)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdClan", pozajmica.IdClan);
                cmd.Parameters.AddWithValue("@IdKnjiga", pozajmica.IdKnjiga);
                cmd.Parameters.AddWithValue("@IdBibliotekar", pozajmica.IdBibliotekar);
                cmd.Parameters.AddWithValue("@DatumPozajmljivanja",pozajmica.DatumPozajmljivanja);
                cmd.Parameters.AddWithValue("@JeRazduzena", pozajmica.JeRazduzena);
                cmd.Parameters.AddWithValue("@Opis", pozajmica.Opis);
                cmd.ExecuteNonQuery();
                pozajmica.IdPozajmica = (int)cmd.LastInsertedId;
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public Pozajmica GetPozajmicaByClanID(int IdClan)
        {
            var result = new Pozajmica();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Pozajmica` WHERE IdClan=@IdClan";
                cmd.Parameters.AddWithValue("@IdClan", IdClan);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Pozajmica()
                {
                    IdPozajmica = reader.GetInt32(0),
                    IdClan = reader.GetInt32(1),
                    IdBibliotekar = reader.GetInt32(2),
                    DatumPozajmljivanja = reader.GetDateTime(3),
                    JeRazduzena = reader.GetBoolean(4),
                    Opis = reader.GetString(5)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlPozajmica", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public void SavePozajmica(Pozajmica pozajmica)
        {
            if (pozajmica.IdPozajmica <= 0)
            {
                InsertPozajmica(pozajmica;
            }
            else
            {
                //UpdatePozajmica(pozajmica);
            }
        }
    }
}
