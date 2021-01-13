using Biblioteka.Data.DataAccess.Exceptions;
using Biblioteka.Data.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Biblioteka.Data.DataAccess.MySql
{
    class MySqlClan
    {
        private static readonly string SELECT = "SELECT * FROM `Clan` ";

        private static readonly string INSERT = "INSERT INTO `Clan`(IdClan, DatumUclanjivanja, DatumObnavljanjaClanstva) " +
                                                            "VALUES (@IdClan, @DatumUclanjivanja, @DatumObnavljanjaClanstva)";

        private static readonly string UPDATE = "UPDATE `Clan` SET DatumObnavljanjaClanstva=@DatumObnavljanjaClanstva WHERE IdClan=@IdClan";

        private static readonly string DELETE = "DELETE FROM `Clan` WHERE IdClan=@IdClan";

        private void InsertClan(Clan clan)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = INSERT;
                cmd.Parameters.AddWithValue("@IdClan", clan.IdClan);
                cmd.Parameters.AddWithValue("DatumUclanjivanja", clan.DatumUclanjivanja);
                cmd.Parameters.AddWithValue("@DatumObnavljanjaClanstva", clan.DatumObnavljanjaClanstva);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlClan", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        private void UpdateClan(Clan clan)
        {
            MySqlConnection con = null;
            MySqlCommand cmd;
            try
            {
                con = MySql.MySqlUtil.GetConnection();
                cmd = con.CreateCommand();
                cmd.CommandText = UPDATE;
                cmd.Parameters.AddWithValue("@DatumObnavljanjaClanstva", clan.DatumObnavljanjaClanstva);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw new DataAccessException("Exception in MySqlClan", exc);
            }
            finally
            {
                MySqlUtil.CloseQuietly(con);
            }
        }

        public void DeleteClanById(int IdClan)
        {
            MySqlConnection conn = null;
            MySqlCommand cmd;
            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = DELETE;
                cmd.Parameters.AddWithValue("@IdOsoba", IdClan);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlClan", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(conn);
            }
        }

        public List<Clan> GetAllClan()
        {
            var result = new List<Clan>();
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
                    result.Add(new Clan()
                    {
                        IdClan = reader.GetInt32(0),
                        DatumUclanjivanja = reader.GetDateTime(1),
                        DatumObnavljanjaClanstva = reader.GetDateTime(2)
                        
                    });
                }
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlClan", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public Clan GetClanByID(int IdClan)
        {
            var result = new Clan();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Clan` WHERE IdClan=@IdClan";
                cmd.Parameters.AddWithValue("@IdOsoba", IdClan);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Clan()
                {
                    IdClan= reader.GetInt32(0),
                    DatumUclanjivanja = reader.GetDateTime(1),
                    DatumObnavljanjaClanstva = reader.GetDateTime(2)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlClan", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }

        public void SaveClan(Clan clan, string action)
        {
            if (action.Equals("insert"))
            {
                InsertClan(clan);
            }
            else if(action.Equals("update"))
            {
                UpdateClan(clan);
            }
        }
    }
}
