using ConfigurationReader.Enums;
using ConfigurationReader.Helpers;

namespace ConfigurationReader.Services
{
    public class ConfigurationBooleanType : IConfigurationType
    {
        public string Type => TypeEnum.Boolean.ToString();

        public object GetValue(string value)
            => value.Parse(TypeEnum.Boolean.GetDescription());
    }
}
