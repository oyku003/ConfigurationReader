using System;
using System.ComponentModel;

namespace ConfigurationReader.Helpers
{
    public static class TypeHelper
    {
       public static object Parse(this string value, string typeName)
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
