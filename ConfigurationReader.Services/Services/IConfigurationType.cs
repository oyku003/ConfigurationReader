namespace ConfigurationReader.Services
{
    public interface IConfigurationType
    {
        string Type { get; }
        object GetValue(string value);
    }
}
