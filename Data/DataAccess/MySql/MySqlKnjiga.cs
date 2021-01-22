using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlKnjiga
    {
        private static readonly string SELECT = "SELECT * FROM `Knjiga` ORDER BY Naslov";

        private static readonly string INSERT = "INSERT INTO `Knjiga`(NazivZanra,IdIzdavac,IdAutor,Naslov, DatumObjavljivanja, ISBN, UkupanBrojKopija, BrojStranica, Jezik, Opis)" +
                                                            "VALUES (@NazivZanra,@IdIzdavac,@IdAutor,@Naslov, @DatumObjavljivanja, @ISBN, @UkupanBrojKopija, @BrojStranica, @Jezik, @Opis)";

        private static readonly string UPDATE = "UPDATE `Knjiga` SET NazivZanra=@NazivZanra, IdIzdavac=@IdIzdavac, IdAutor=@IdAutor, Naslov=@Naslov, DatumObjavljivanja=@DatumObjavljivanja,ISBN=@ISBN, UkupanBrojKopija=@UkupanBrojKopija," +
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
                cmd.Parameters.AddWithValue("@NazivZanra",knjiga.NazivZanra);
                cmd.Parameters.AddWithValue("@IdIzdavac",knjiga.IdIzdavac);
                cmd.Parameters.AddWithValue("@IdAutor", knjiga.IdAutor);
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
                cmd.Parameters.AddWithValue("@NazivZanra", knjiga.NazivZanra);
                cmd.Parameters.AddWithValue("@IdIzdavac", knjiga.IdIzdavac);
                cmd.Parameters.AddWithValue("@IdAutor", knjiga.IdAutor);
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
                        NazivZanra = reader.GetString(1),
                        IdIzdavac = reader.GetInt32(2),
                        IdAutor = reader.GetInt32(3),
                        Naslov = reader.GetString(4),
                        DatumObjavljivanja = reader.GetDateTime(5),
                        ISBN = reader.GetString(6),
                        UkupanBrojKopija = reader.GetInt32(7),
                        BrojStranica = reader.GetInt32(8),
                        Jezik = reader.GetString(9),
                        Opis = reader.GetString(10)
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

        public DataTable GetKnjigaAutorZanrIzdavacJoin(string naslov, string zanr, string izdavac, string autor)
        {
            var table = new DataTable();
            //Define columns
            var IdKnjiga = new DataColumn("Šifra", typeof(int));
            var Dostupnost = new DataColumn("Dostupnih kopija", typeof(string));
            var Naslov = new DataColumn("Naslov", typeof(string));
            var NazivZanra = new DataColumn("Žanr", typeof(string));
            var Izdavac = new DataColumn("Izdavač", typeof(string));
            var ImeAutora = new DataColumn("Ime autora", typeof(string));
            var PrezimeAutora = new DataColumn("Prezime autora", typeof(string));
            var DatumObjavljivanja = new DataColumn("Datum objavljivanja", typeof(string));
            var ISBN = new DataColumn("ISBN", typeof(string));
            var BrojKopija = new DataColumn("Broj kopija", typeof(int));
            var BrojStranica = new DataColumn("Broj stranica", typeof(int));
            var Jezik = new DataColumn("Jezik", typeof(string));
            //Add columns to a table
            table.Columns.Add(IdKnjiga);
            table.Columns.Add(Dostupnost);
            table.Columns.Add(Naslov);
            table.Columns.Add(NazivZanra);
            table.Columns.Add(Izdavac);
            table.Columns.Add(ImeAutora);
            table.Columns.Add(PrezimeAutora);
            table.Columns.Add(DatumObjavljivanja);
            table.Columns.Add(ISBN);
            table.Columns.Add(BrojKopija);
            table.Columns.Add(BrojStranica);
            table.Columns.Add(Jezik);

            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                string query = "SELECT IdKnjiga AS Sifra, Naslov,NazivZanra,Izdavac.Naziv AS Izdavac,Autor.Ime AS Ime_autora,Autor.Prezime AS Prezime_autora,DatumObjavljivanja,ISBN,UkupanBrojKopija,BrojStranica,Jezik " +
                "FROM Knjiga INNER JOIN Izdavac ON Izdavac.IdIzdavac = Knjiga.IdIzdavac INNER JOIN Autor ON Autor.IdAutor = Knjiga.IdAutor WHERE Naslov LIKE @Naslov AND NazivZanra LIKE @Naziv AND Izdavac.Naziv LIKE @Izdavac AND Autor.Prezime LIKE @Autor";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@Naslov", naslov + "%");
                cmd.Parameters.AddWithValue("@Naziv", zanr + "%");
                cmd.Parameters.AddWithValue("@Izdavac", izdavac + "%");
                cmd.Parameters.AddWithValue("@Autor", autor + "%");
                reader = cmd.ExecuteReader();

                var mysqlPozajmica = new MySqlPozajmica();
                var mysqlKnjiga = new MySqlKnjiga();

                while (reader.Read())
                {
                    int brojPozajmica = mysqlPozajmica.GetUkupanBrojPozajmicaByKnjigaId(reader.GetInt32(0));
                    int brojKopija = reader.GetInt32(8);
                    var row = table.NewRow();

                    row[0] = reader.GetInt32(0);
                    if (brojPozajmica < brojKopija)
                        row[1] = (brojKopija-brojPozajmica);
                    else
                        row[1] = "NEDOSTUPNA";
                    
                    row[2] = reader.GetString(1);
                    row[3] = reader.GetString(2);
                    row[4] = reader.GetString(3);
                    row[5] = reader.GetString(4);
                    row[6] = reader.GetString(5);
                    row[7] = reader.GetDateTime(6).ToShortDateString();
                    row[8] = reader.GetString(7);
                    row[9] = reader.GetInt16(8);
                    row[10] = reader.GetInt16(9);
                    row[11] = reader.GetString(10);
                    table.Rows.Add(row);
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
            return table;
        }

        public List<Knjiga> GetAllKnjigaByNaslov(string naslov)
        {
            var result = new List<Knjiga>();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Knjiga` WHERE Naslov LIKE @Naslov ";
                cmd.Parameters.AddWithValue("@Naslov", naslov+"%");
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Knjiga()
                    {
                        IdKnjiga = reader.GetInt32(0),
                        NazivZanra = reader.GetString(1),
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
                    NazivZanra = reader.GetString(1),
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

        public int GetBrojNaslova()
        {
            var count = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM `Knjiga`";
                reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);
                
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return count;
        }

        public int GetBrojKopija()
        {
            var count = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT SUM(UkupanBrojKopija) FROM `Knjiga`";
                reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);

            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return count;
        }
        public int GetBrojKopijaById(int id)
        {
            var count = 0;
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT SUM(UkupanBrojKopija) FROM `Knjiga` WHERE IdKnjiga=@IdKnjiga";
                cmd.Parameters.AddWithValue("@IdKnjiga", id);
                reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);

            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlKnjiga", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return count;
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
