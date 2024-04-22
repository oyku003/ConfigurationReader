using ConfigurationReader.Background.Events;
using ConfigurationReader.Background.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationReader.Background.Consumers
{
    public class ServiceConfigurationStorageChangedEventConsumer : IConsumer<ServiceConfigurationStorageChangedEvent>
    {
        private readonly RedisService _redisService;
        private readonly ILogger<ServiceConfigurationStorageCreatedEventConsumer> _logger;
        public ServiceConfigurationStorageChangedEventConsumer(RedisService redisService, ILogger<ServiceConfigurationStorageCreatedEventConsumer> logger)
        {
            _redisService = redisService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ServiceConfigurationStorageChangedEvent> context)
        {
            if (context.Message == default)
            {
                return;
            }

            try
            {
                _logger.LogInformation($"{nameof(context.Message)} is started.");
                var @event= context.Message;
                _redisService.GetDb().HashDelete(@event.ApplicationName, @event.Id);
                _redisService.GetDb().HashSet(@event.ApplicationName, @event.Id, JsonSerializer.Serialize(@event));
                _logger.LogInformation($"{nameof(context.Message)} is finished.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(context.Message)} error occured. Error: {ex.Message}");
            }
        }
    }
}
