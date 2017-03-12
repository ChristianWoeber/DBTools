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
        internal static object Create(IDataReader rd)
        {
            var fields = new List<string>();

            foreach (var item in typeof(T).GetProperties())
            {
                var lstAttributes = (IList<CustomAttributeData>)item.CustomAttributes;
                if (lstAttributes.Count > 0)
                {
                    var attr = lstAttributes[0].NamedArguments[0].TypedValue.Value.ToString();
                    fields.Add(attr);
                }
            }
            return null;
        }
    }
}