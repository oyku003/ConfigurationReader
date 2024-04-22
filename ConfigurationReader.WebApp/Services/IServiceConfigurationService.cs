using ConfigurationReader.Shared.Models.Dtos;
using ConfigurationReader.Shared.Models.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationReader.WebApp.Services
{
    public interface IServiceConfigurationService
    {
        Task<List<ServiceConfigurationDto>> GetAsync();
        Task<ServiceConfigurationDto> GetByIdAsync(int id);
        Task CreateAsync(CreateServiceConfigurationRequest createServiceConfigurationRequest);
        Task UpdateAsync(UpdateServiceConfigurationRequest updateServiceConfigurationRequest);
        Task DeleteAsync(int id);
    }
}
