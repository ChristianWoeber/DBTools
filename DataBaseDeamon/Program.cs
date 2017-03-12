using MySql.Data.MySqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;

namespace DataBaseDeamon
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLCmd.Connection = DataBaseFactory.Create(new MySqlConnection());
            //  SQLCmd.Insert("trading_test", "Securities").Values("NAME", "TEST2");

            //insert into trading_test.indices (NAME,DESCRIPTION,ASSETCLASS_ID)values('TEST','DAS IST EINE DESCRIPTIOID_N',100)
            //SQLCmd.Insert("trading_test", "indices").Values(
            //    "NAME", "TEST",
            //    "DESCRIPTION", "DAS IST EINE DESCRIPTION",
            //    "ASSETCLASS_ID", 10);

            var lstIndices = new List<Indices>();

            var cmd = SQLCmd.Select("trading_test", "indices").Fields("NAME,DESCRIPTION,ASSETCLASS_ID");

            foreach (Indices item in cmd.QueryObjects<Indices>())
            {
                lstIndices.Add(item);
            }

            SQLCmd.Connection.Dispose();
          //  SQLCmd.Execute();
            //var sqlCmd = new MySQLCommandBuilder();
            //sqlCmd.Create();
            //sqlCmd.Insert("trading_test", "Securities").Values("NAME", "TEST");


            //DataBaseHelper.OpenConnection();

        }
    }
    public class Indices
    {
        [Column(Storage = "ASSETCLASS_ID")]
        public int Assetclass { get; set; }
        [Column(Storage = "NAME")]
        public string Name { get; set; }
        [Column(Storage = "DESCRIPTION")]
        public string Description { get; set; }
    }

}
