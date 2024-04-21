using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Services.Helpers
{
    public static class TypeHelper
    {
       public static object Parse(string typeName, string value)
        {
            var type = Type.GetType(typeName);
            var converter = TypeDescriptor.GetConverter(type);

            if (converter.CanConvertFrom(typeof(string)))
            {
                return converter.ConvertFromInvariantString(value);
            }

            return null;
        }
    }
}
