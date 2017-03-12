namespace DataBaseDeamon
{

    public interface ISqlCmdText
    {
        string CreateCmd(string db, string table);
    }
}