using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace ConfigurationReader.WebApp.Helpers
{
    public static class EnumHelper
    {
        public static bool HasDescriptionByEnum<T>(this string description)
        {
            var type = typeof(T);

            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach(var field in from field in type.GetFields()
                                let attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute
                                where attribute != null
                                where attribute.Description == description
                                select field)
            {
                return true;
            }

            return false;
        }


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
