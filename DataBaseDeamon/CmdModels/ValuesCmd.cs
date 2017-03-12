using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBaseDeamon
{
    public class ValuesCmd : ISqlValueCmd
    {
        public string CreateCmd(params object[] values)
        {
            //triviale Implementierung gehe von value pairs aus 1 FieldName, 2 Value, FieldName, Value //
            // daher werden alle Fieldnames und alle Values gesammelt um folgende Syntax zu erhalten//
            // (Fieldname1,Fieldnmae2)Values(Value1,value2)

            if (values.Length > 2)
                return MultipleValuesCmd(values);

            return SingleValuesCmd(values);
        }

        private string SingleValuesCmd(object[] values)
        {
            var sb = new StringBuilder();
            sb.Append(" VALUES(");
            var cnt = 0;
            foreach (var item in values)
            {
                cnt++;
                if (cnt % 2 == 0)
                {
                    sb.Append($"'{item}'");
                }
                else
                {
                    sb.Append($"{item}, ");
                }
            }
            sb.Append(")");
            return sb.ToString();
        }
        private List<KeyValuePair<string, object>> _lsKeyValue = new List<KeyValuePair<string, object>>();

        private string MultipleValuesCmd(object[] values)
        {
            for (int i = 0; i < values.Length; i += 2)           
                _lsKeyValue.Add(new KeyValuePair<string, object>((string)values[i], values[i + 1]));
            
            return BuildCmdString();
        }

        private string BuildCmdString()
        {
            var sbFields = new StringBuilder();
            var sbValues = new StringBuilder();
            sbFields.Append("(");
            sbValues.Append("(");

            for (int i = 0; i < _lsKeyValue.Count; i++)
            {
                if (i == _lsKeyValue.Count - 1)
                {
                    sbFields.Append($"{_lsKeyValue[i].Key}");
                    sbValues.Append($"{ParseValue(_lsKeyValue[i].Value)}");
                }
                else
                {
                    sbFields.Append($"{_lsKeyValue[i].Key},");
                    sbValues.Append($"{ParseValue(_lsKeyValue[i].Value)},");
                }
            }

            sbFields.Append(")");
            sbValues.Append(")");

            return $"{sbFields} values{sbValues}";
        }

        private string ParseValue(object value)
        {
            if (value is string)
            {
                var tmp = value as string;
                return $"'{tmp}'";
            }
            else if (value is DateTime)
            {
                var tmp = (DateTime)value;
                return tmp.ToString();
            }
            else if (value is int)
            {
                var tmp = (int)value;
                return tmp.ToString();
            }
            else if (value is decimal)
            {
                var tmp = (decimal)value;
                return tmp.ToString();
            }
            else
                return null;
        }
    }
}
