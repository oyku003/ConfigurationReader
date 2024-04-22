using ConfigurationReader.Enums;
using ConfigurationReader.Helpers;
namespace ConfigurationReader.Services
{
    public class ConfigurationIntegerType : IConfigurationType
    {
        public string Type => TypeEnum.Int.ToString();

        public object GetValue(string value)
            => value.Parse(TypeEnum.Int.GetDescription());
    }
}
