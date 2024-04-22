using ConfigurationReader.Data.Entities;
using ConfigurationReader.Interfaces;
using ConfigurationReader.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationReader.Services
{
    public class ConfigurationReaderService : IConfigurationReaderService
    {
        private readonly RedisService _redisService;
        private readonly IReadOnlyRepository<ServiceConfiguration> _readOnlyRepository;
        private readonly string _applicationName;
        private readonly IServiceProvider _serviceProvider;

        public ConfigurationReaderService(RedisService redisService, IReadOnlyRepository<ServiceConfiguration> readOnlyRepository, IOptions<ApplicationInfoSetting> options, IServiceProvider serviceProvider)
        {
            _redisService = redisService;
            _readOnlyRepository = readOnlyRepository;
            _applicationName = options.Value.ApplicationName;
            _serviceProvider = serviceProvider;
        }

        public async Task<object> GetValueAsync(string key)
        {
            var existConfiguration = await _redisService.GetDb().HashGetAsync(_applicationName, key);
            var services = _serviceProvider.GetServices<IConfigurationType>();

            if (!string.IsNullOrEmpty(existConfiguration))
            {
                var serviceConfiguration = JsonSerializer.Deserialize<ServiceConfiguration>(existConfiguration);

                return services.FirstOrDefault(x => x.Type == serviceConfiguration.Type)?.GetValue(serviceConfiguration.Value);
            }

            var entity = (await _readOnlyRepository.SingleOrDefaultAsync(x => x.Name == key && x.IsActive == 1));

            return services.FirstOrDefault(x => x.Type == entity.Type)?.GetValue(entity.Value);
        }

        public async Task<T> GetValue<T>(string key)
        {
            var existConfiguration = await _redisService.GetDb().HashGetAsync(_applicationName, key);

            if (!string.IsNullOrEmpty(existConfiguration))
            {
                return JsonSerializer.Deserialize<T>(existConfiguration);
            }

            return JsonSerializer.Deserialize<T>((await _readOnlyRepository.SingleOrDefaultAsync(x => x.Name == key && x.IsActive == 1)).Value);
        }
    }
}

