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
    class MySqlAdmin
    {
        public Administrator GetBibliotekarByUsername(string korisnickoIme)
        {
            var result = new Administrator();
            MySqlConnection conn = null;
            MySqlCommand cmd;
            MySqlDataReader reader = null;

            try
            {
                conn = MySqlUtil.GetConnection();
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM `Administrator` WHERE KorisnickoIme=@KorisnickoIme";
                cmd.Parameters.AddWithValue("@KorisnickoIme", korisnickoIme);
                reader = cmd.ExecuteReader();
                reader.Read();
                result = new Administrator()
                {
                    IdAdministrator = reader.GetInt32(0),
                    KorisnickoIme = reader.GetString(1),
                    Lozinka = reader.GetString(2)
                };
            }
            catch (Exception ex)
            {
                throw new DataAccessException("Exception in MySqlBibliotekar", ex);
            }
            finally
            {
                MySqlUtil.CloseQuietly(reader, conn);
            }
            return result;
        }
    }
}
