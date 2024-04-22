using System.Threading.Tasks;

namespace ConfigurationReader.Services
{
    public interface IConfigurationReaderService
    {
        Task<object> GetValueAsync(string key);
        Task<T> GetValue<T>(string key);
    }
}
