namespace DataBaseDeamon
{

    public class InsertCmd : ISqlCmdText
    {
        public string CreateCmd(string db, string table)
        {
            return $"INSERT INTO {db}.{table}";
        }
    }
}