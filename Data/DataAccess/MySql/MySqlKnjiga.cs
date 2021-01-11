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
    class MySqlKnjiga
    {
        private static readonly string SELECT = "SELECT * FROM `Knjiga` ORDER BY Naslov";

        private static readonly string INSERT = "INSERT INTO `Knjiga`(IdZanr,IdIzdavac,Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis)" +
                                                            "VALUES (@IdZanr,@IdIzdavac,@Naslov, @DatumRodjenja, @ISBN, @UkupanBrojKopija, @BrojStranica, @Jezik, @Opis)";

        private static readonly string UPDATE = "UPDATE `Knjiga` SET IdZanr=@IdZanr, IdIzdavac=@IdIzdavac, Naslov=@Naslov, DatumObjavljivanja=@DatumObjavljivanja,ISBN=@ISBN, UkupanBrojKopija=@UkupanBrojKopija," +
                                                " BrojStranica=@BrojStranica, Jezik=@Jezik, Opis=@Opis WHERE IdKnjiga=@IdKnjiga";

        private static readonly string DELETE = "DELETE FROM `Knjiga` WHERE IdKnjiga=@IdKnjiga";

        private void InsertKnjiga(Knjiga knjiga)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdZanr",knjiga.IdZanr);
                cmd.Parameters.AddWithValue("@IdIzdavac",knjiga.IdIzdavac);
                cmd.Parameters.AddWithValue("@Naslov",knjiga.Naslov);
                cmd.Parameters.AddWithValue("@DatumObjavljivanja",knjiga.DatumObjavljivanja);
                cmd.Parameters.AddWithValue("@ISBN",knjiga.ISBN);
                cmd.Parameters.AddWithValue("@UkupanBrojKopija",knjiga.UkupanBrojKopija);
                cmd.Parameters.AddWithValue("@BrojStranica",knjiga.BrojStranica);
                cmd.Parameters.AddWithValue("@Jezik",knjiga.Jezik);
                cmd.Parameters.AddWithValue("@Opis",knjiga.Opis);
                cmd.ExecuteNonQuery();
                knjiga.IdKnjiga = (int)cmd.LastInsertedId;
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateKnjiga(Knjiga knjiga)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@IdKnjiga", knjiga.IdKnjiga);
                cmd.Parameters.AddWithValue("@IdZanr", knjiga.IdZanr);
                cmd.Parameters.AddWithValue("@IdIzdavac", knjiga.IdIzdavac);
                cmd.Parameters.AddWithValue("@Naslov", knjiga.Naslov);
                cmd.Parameters.AddWithValue("@DatumObjavljivanja", knjiga.DatumObjavljivanja);
                cmd.Parameters.AddWithValue("@ISBN", knjiga.ISBN);
                cmd.Parameters.AddWithValue("@UkupanBrojKopija", knjiga.UkupanBrojKopija);
                cmd.Parameters.AddWithValue("@BrojStranica", knjiga.BrojStranica);
                cmd.Parameters.AddWithValue("@Jezik", knjiga.Jezik);
                cmd.Parameters.AddWithValue("@Opis", knjiga.Opis);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public void DeleteKnjigaById(int IdKnjiga)
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
                throw new DataAccessException("Exception in MySqlKnjiga", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Knjiga> GetAllKnjiga()
        {
            var result = new List<Knjiga>();
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
                    result.Add(new Knjiga()
                    {
                        IdKnjiga = reader.GetInt32(0),
                        IdZanr = reader.GetInt32(1),
                        IdIzdavac = reader.GetInt32(2),
                        Naslov = reader.GetString(3),
                        DatumObjavljivanja = reader.GetDateTime(4),
                        ISBN = reader.GetString(5),
                        UkupanBrojKopija = reader.GetInt32(6),
                        BrojStranica = reader.GetInt32(7),
                        Jezik = reader.GetString(8),
                        Opis = reader.GetString(9)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public Knjiga GetKnjigaByID(int IdKnjiga)
        {
            var result = new Knjiga();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Knjiga` WHERE IdKnjiga=@IdKnjiga";
                cmd.Parameters.AddWithValue("@IdKnjiga", IdKnjiga);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Knjiga()
                {
                    IdKnjiga = reader.GetInt32(0),
                    IdZanr = reader.GetInt32(1),
                    IdIzdavac = reader.GetInt32(2),
                    Naslov = reader.GetString(3),
                    DatumObjavljivanja = reader.GetDateTime(4),
                    ISBN = reader.GetString(5),
                    UkupanBrojKopija = reader.GetInt32(6),
                    BrojStranica = reader.GetInt32(7),
                    Jezik = reader.GetString(8),
                    Opis = reader.GetString(9)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public void SaveKnjiga(Knjiga knjiga)
        {
            if (knjiga.IdKnjiga <= 0)
            {
                InsertKnjiga(knjiga);
            }
            else
            {
                UpdateKnjiga(knjiga);
            }
        }
    }
}
