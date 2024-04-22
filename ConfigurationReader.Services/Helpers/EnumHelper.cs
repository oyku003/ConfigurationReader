using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum enumeration)
           => enumeration.GetAttributeValue<DescriptionAttribute, string>(o => o.Description);

        public static TExpected GetAttributeValue<T, TExpected>(this Enum enumeration, Func<T, TExpected> expression)
              where T : Attribute
        {
            var attribute =
                enumeration.GetType()
                    .GetMember(enumeration.ToString())
                    .FirstOrDefault(member => member.MemberType == MemberTypes.Field)
                    ?.GetCustomAttributes(typeof(T), false)
                    .Cast<T>()
                    .SingleOrDefault();

            return attribute == null ? default(TExpected) : expression(attribute);
        }
    }
}
