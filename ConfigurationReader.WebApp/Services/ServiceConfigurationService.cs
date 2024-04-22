using ConfigurationReader.Shared.Models.Dtos;
using ConfigurationReader.Shared.Models.Requests;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ConfigurationReader.WebApp.Services
{
    public class ServiceConfigurationService: IServiceConfigurationService
    {
        private readonly HttpClient _client;

        public ServiceConfigurationService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateAsync(CreateServiceConfigurationRequest createServiceConfigurationRequest)
            => await _client.PostAsJsonAsync("api/ConfigurationReaders", createServiceConfigurationRequest);       

        public async Task<List<ServiceConfigurationDto>> GetAsync()
        => await _client.GetFromJsonAsync<List<ServiceConfigurationDto>>("api/ConfigurationReaders");
        public async Task<ServiceConfigurationDto> GetByIdAsync(int id)
        => await _client.GetFromJsonAsync<ServiceConfigurationDto>( $"api/ConfigurationReaders/{id}");

        public async Task UpdateAsync(UpdateServiceConfigurationRequest updateServiceConfigurationRequest)
            => await _client.PutAsJsonAsync("api/ConfigurationReaders", updateServiceConfigurationRequest);
        
        public async Task DeleteAsync(int id)
            => await _client.DeleteAsync($"api/ConfigurationReaders/{id}");

    }
}
