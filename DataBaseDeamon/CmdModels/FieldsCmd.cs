using System;
using System.Text;

namespace DataBaseDeamon
{
    public class FieldsCmd : ISqlValueCmd
    {
        public string CreateCmd(params object[] fields)
        {
            var sb = new StringBuilder();
            if (fields.Length == 1)
            {
                sb.Append($"{fields[0]}");
            }
            else
            {
                for (int i = 0; i < fields.Length; i++)
                {
                    if (i == fields.Length - 1)
                        sb.Append($"{fields[i]}");
                    else
                        sb.Append($"{fields[i]},");
                }
            }
            return sb.ToString();
        }
    }
}