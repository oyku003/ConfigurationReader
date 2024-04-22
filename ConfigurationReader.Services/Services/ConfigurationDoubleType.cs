using ConfigurationReader.Enums;
using ConfigurationReader.Helpers;

namespace ConfigurationReader.Services
{
    public class ConfigurationDoubleType : IConfigurationType
    {
        public string Type => TypeEnum.Double.ToString();

        public object GetValue(string value)
            => value.Parse(TypeEnum.Double.GetDescription());
    }
}
