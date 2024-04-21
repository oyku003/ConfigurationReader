using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ConfigurationReader.Services.Extensions
{
    public static class AttributeExtension
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

            return attribute == null ? default : expression(attribute);
        }

        public static T ParseEnum<T>(this string value) => (T)Enum.Parse(typeof(T), value, true);
    }
}
