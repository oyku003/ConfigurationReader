using ConfigurationReader.Background.Events;
using ConfigurationReader.Background.Services;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConfigurationReader.Background.Consumers
{
    public class ServiceConfigurationStorageDeletedEventConsumer : IConsumer<ServiceConfigurationStorageDeletedEvent>
    {
        private readonly RedisService _redisService;
        private readonly ILogger<ServiceConfigurationStorageDeletedEventConsumer> _logger;
        public ServiceConfigurationStorageDeletedEventConsumer(RedisService redisService, ILogger<ServiceConfigurationStorageDeletedEventConsumer> logger)
        {
            _redisService = redisService;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<ServiceConfigurationStorageDeletedEvent> context)
        {
            if (context.Message == default)
            {
                return;
            }

            try
            {
                _logger.LogInformation($"{nameof(context.Message)} is started.");
                var @event = context.Message;
                _redisService.GetDb().HashDelete(@event.ApplicationName, @event.Id);
                _logger.LogInformation($"{nameof(context.Message)} is finished.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(context.Message)} error occured. Error: {ex.Message}");
            }
        }
    }
}



