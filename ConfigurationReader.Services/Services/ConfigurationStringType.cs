using ConfigurationReader.Enums;
using ConfigurationReader.Helpers;

namespace ConfigurationReader.Services
{
    public class ConfigurationStringType : IConfigurationType
    {
        public string Type => TypeEnum.String.ToString();
        public object GetValue(string value)
            => value.Parse(TypeEnum.String.GetDescription());        
    }
}
