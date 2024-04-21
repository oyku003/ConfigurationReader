namespace ConfigurationReader.Api.Services.Cqrs.Commands
{
    public abstract class BaseServiceConfigurationCommand
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }
    }
}
