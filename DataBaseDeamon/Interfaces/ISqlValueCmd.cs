namespace DataBaseDeamon
{
    public interface ISqlValueCmd
    {
        string CreateCmd(params object[] values);
    }
}