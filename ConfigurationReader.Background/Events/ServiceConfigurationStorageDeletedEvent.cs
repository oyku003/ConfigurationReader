
namespace ConfigurationReader.Background.Events
{
    public class ServiceConfigurationStorageDeletedEvent
    {
        public int Id { get; set; }
        public string ApplicationName { get; set; }
    }
}
