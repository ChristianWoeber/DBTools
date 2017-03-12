using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseDeamon
{

    public class MySQLCommandBuilder
    {
        private Dictionary<SQLCommandTypes, ISqlCmdText> _cmdCache;
        private Dictionary<SQLValueTypes, ISqlValueCmd> _cmdValuesCache;
        public StringBuilder CmdBuilder = new StringBuilder();
        public readonly SQLCommandTypes SQLCmdType;
        public SQLValueTypes SQLValueType;

        public MySQLCommandBuilder(SQLCommandTypes cmdtype, string db, string table)
        {
            SQLCmdType = cmdtype;
            GetCmdText(cmdtype, db, table);
        }

        private string GetCmdText(SQLCommandTypes cmdtype, string db, string table)
        {
            if (_cmdCache == null)
                LoadCache();

            var ret = _cmdCache.ContainsKey(cmdtype) ? _cmdCache[cmdtype].CreateCmd(db, table) : null;
            CmdBuilder.Append(ret);
            return ret;
        }

        private void LoadCache()
        {
            _cmdCache = new Dictionary<SQLCommandTypes, ISqlCmdText>();
            foreach (SQLCommandTypes cmdEnum in typeof(SQLCommandTypes).GetEnumValues())
            {
                switch (cmdEnum)
                {
                    case SQLCommandTypes.Insert:
                        if (!_cmdCache.ContainsKey(cmdEnum))
                            _cmdCache.Add(cmdEnum, new InsertCmd());
                        break;
                    case SQLCommandTypes.Delete:
                        break;
                    case SQLCommandTypes.Update:
                        break;
                    case SQLCommandTypes.Select:
                        if (!_cmdCache.ContainsKey(cmdEnum))
                            _cmdCache.Add(cmdEnum, new SelectCmd());
                        break;
                    default:
                        break;
                }
            }
        }

        public void Fields(string[] fields)
        {
            SQLValueType = SQLValueTypes.Fields;
            CreateFieldsCmdText(SQLValueTypes.Fields, fields);
        }

        private string CreateFieldsCmdText(SQLValueTypes cmdType, string[] fields)
        {
            if (_cmdValuesCache == null)
                LoadValuesCache();

            var ret = _cmdValuesCache.ContainsKey(cmdType) ? _cmdValuesCache[cmdType].CreateCmd(fields) : null;
            CmdBuilder.Replace("@", ret);                   
            return ret;
        }

        public void Values(params string[] values)
        {
            SQLValueType = SQLValueTypes.Values;
            CreateValuesCmdText(SQLValueTypes.Values, values);
        }

        private string CreateValuesCmdText(SQLValueTypes cmdType, object[] values)
        {
            if (_cmdValuesCache == null)
                LoadValuesCache();

            var ret = _cmdValuesCache.ContainsKey(cmdType) ? _cmdValuesCache[cmdType].CreateCmd(values) : null;
            CmdBuilder.Append(ret);
            return ret;

        }

        private void LoadValuesCache()
        {
            _cmdValuesCache = new Dictionary<SQLValueTypes, ISqlValueCmd>();
            foreach (SQLValueTypes cmdEnum in typeof(SQLValueTypes).GetEnumValues())
            {
                switch (cmdEnum)
                {
                    case SQLValueTypes.Values:
                        if (!_cmdValuesCache.ContainsKey(cmdEnum))
                            _cmdValuesCache.Add(cmdEnum, new ValuesCmd());
                        break;
                    case SQLValueTypes.Fields:
                        if (!_cmdValuesCache.ContainsKey(cmdEnum))
                            _cmdValuesCache.Add(cmdEnum, new FieldsCmd());
                        break;
                    default:
                        break;
                }
            }
        }

        internal void CreateValueTypesCmd(SQLValueTypes type, object[] values)
        {
            CreateValuesCmdText(type, values);
        }
    }
}