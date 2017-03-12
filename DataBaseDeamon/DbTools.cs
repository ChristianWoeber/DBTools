using System;
using System.Data;
using System.Data.Common;

namespace DataBaseDeamon
{
    internal class DbTools
    {
        private static DbCommand _dbCmd { get; set; }
        private static DbConnection _dbCon { get; set; }

        internal static void Exec(DbConnection con, MySQLCommandBuilder builder)
        {
            try
            {
                using (_dbCon = con)
                using (_dbCmd = con.CreateCommand())
                {
                    if (builder.SQLCmdType == SQLCommandTypes.Insert || builder.SQLCmdType == SQLCommandTypes.Delete
                        || builder.SQLCmdType == SQLCommandTypes.Update)
                    {
                        _dbCmd.CommandText = builder.CmdBuilder.ToString();

                        //cmd.CommandText = "INSERT INTO Authors(Name) VALUES(@Name)";
                        //  cmd.Prepare();
                        //cmd.Parameters.AddWithValue("@Name", "Trygve Gulbranssen");

                        _dbCmd.ExecuteNonQuery();
                    }

                    Console.WriteLine($"folgendes Command erfolgreich ausgeführt: {_dbCmd.CommandText}");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            //finally
            //{
            //    if (_dbCmd != null)
            //        _dbCmd.Dispose();
            //    if (_dbCon != null)
            //        _dbCon.Dispose();
            //}
        }

        public static IDataReader CreateQuery(SQLCmd sqlCmd, DbConnection con, MySQLCommandBuilder builder)
        {
            try
            {
                _dbCon = con;
                _dbCmd = con.CreateCommand();                
                _dbCmd.CommandText = builder.CmdBuilder.ToString();
                return _dbCmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}