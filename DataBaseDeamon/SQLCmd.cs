using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DataBaseDeamon
{

    public class SQLCmd
    {
        public static DbConnection Connection { get; set; }
        private static string _db;
        private static string _table;
        private static MySQLCommandBuilder _builder;
        private SQLCommandTypes cmdTpye;

        public SQLCmd(DbConnection connection, SQLCommandTypes cmdtype)
        {
            Connection = connection;
            cmdTpye = cmdtype;
            _builder = new MySQLCommandBuilder(cmdTpye, _db, _table);
        }

        public static SQLCmd Select(string database, string table)
        {
            CheckConnection();
            _db = database;
            _table = table;
            var cmd = new SQLCmd(Connection, SQLCommandTypes.Select);
            return cmd;
        }

        public SQLCmd Fields(params string[] fields)
        {
            _builder.Fields(fields);
            return this;
        }

        public static IDataReader Query(SQLCmd sqlCmd)
        {
            return DbTools.CreateQuery(sqlCmd, Connection, _builder);
        }

        public IEnumerable<object> QueryObjects<T>()
        {
            using (var rd = Query(this))
            {
                while (rd.Read()) yield return ObjectBinder<T>.Create(rd);
            }
        }

        public static SQLCmd Insert(string database, string table)
        {
            CheckConnection();
            _db = database;
            _table = table;
            var cmd = new SQLCmd(Connection, SQLCommandTypes.Insert);
            return cmd;
        }

        public SQLCmd Values(params object[] values)
        {
            if (values.Length % 2 != 0)
                throw new ArgumentException("Es wird ein KeyValue-Pair erwartet");
            for (int i = 0; i < values.Length; i += 2)
            {
                if (!(values[i] is string))
                    throw new ArgumentException("Der FieldnameWert muss vom Typ String sein");
            }
            _builder.CreateValueTypesCmd(SQLValueTypes.Values, values);
            return this;
        }

        private static void CheckConnection()
        {
            if (Connection == null)
                throw new Exception("Achtung noch keine Datenbankverbindung hergestellt");
            else if (Connection.State != ConnectionState.Open)
                throw new Exception("Achtung Connection ist nicht offen");
        }

        public static void Execute()
        {
            DbTools.Exec(Connection, _builder);
        }
    }
}