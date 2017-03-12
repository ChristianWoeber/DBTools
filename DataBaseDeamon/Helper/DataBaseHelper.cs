using System;
using MySql.Data.MySqlClient;

namespace DataBaseDeamon
{

    public class DataBaseHelper
    {
        private static MySqlConnection _con;
        public static void OpenConnection()
        {
            try
            {
                using (_con = new MySqlConnection())
                {
                    _con.ConnectionString = Settings.Default.DataBaseConnectionString;
                    _con.Open();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}