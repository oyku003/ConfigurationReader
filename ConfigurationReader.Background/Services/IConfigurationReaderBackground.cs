using System.Threading.Tasks;

namespace ConfigurationReader.Background.Services
{
    public interface IConfigurationReaderBackground
    {
        Task SetStorage();
    }
}
