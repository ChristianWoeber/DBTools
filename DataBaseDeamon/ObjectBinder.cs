using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;

namespace DataBaseDeamon
{
    internal class ObjectBinder<T>
    {
        internal static T Create(IDataReader rd)
        {
            var obj = Activator.CreateInstance<T>();
            foreach (var item in typeof(T).GetProperties())
            {
                var lstAttributes = (IList<CustomAttributeData>)item.CustomAttributes;
                if (lstAttributes.Count > 0)
                {
                    var attr = lstAttributes[0].NamedArguments[0].TypedValue.Value.ToString();
                    //value from DB as object
                    var dbValue = rd[attr];
                    var propertyValue = Convert.ChangeType(dbValue, item.PropertyType);
                    item.SetValue(obj, propertyValue);

                    //yield return new KeyValuePair<string, object>(attr, dbValue);
                }
            }
            return obj;

            //return null;
        }

        private static object Parse(Type propertyType, object dbValue)
        {
            if (propertyType == typeof(int))
            {
                var tmp = (int)dbValue;
                return tmp;
            }
            else if (propertyType == typeof(string))
            {
                var tmp = (string)dbValue;
                return tmp;
            }
            else if (propertyType == typeof(DateTime))
            {
                var tmp = (DateTime)dbValue;
                return tmp;
            }
            else if (propertyType == typeof(decimal))
            {
                var tmp = (decimal)dbValue;
                return tmp;
            }
            else if (propertyType == typeof(double))
            {
                var tmp = (double)dbValue;
                return tmp;
            }
            else
                return null;
        }
    }
}