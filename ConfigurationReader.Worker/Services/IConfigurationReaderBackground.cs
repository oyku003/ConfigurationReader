using System.Threading.Tasks;

namespace ConfigurationReader.Worker.Services
{
    public interface IConfigurationReaderBackground
    {
        Task SetStorage();
    }
}
