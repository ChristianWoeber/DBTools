using System;

namespace DataBaseDeamon
{
    internal class SelectCmd : ISqlCmdText
    {
        public string CreateCmd(string db, string table)
        {
            return $"SELECT @ from {db}.{table}";
        }
    }
}