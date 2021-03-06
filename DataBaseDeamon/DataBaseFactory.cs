﻿using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DataBaseDeamon
{
    internal class DataBaseFactory
    {
        internal static DbConnection Create(DbConnection connection)
        {
            if (connection.GetType() == typeof(MySqlConnection))
            {
                try
                {
                    var con = new MySqlConnection();
                    con.ConnectionString = Settings.Default.DataBaseConnectionString;
                    con.Open();
                    return con;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            throw new NotImplementedException();
        }
    }
}