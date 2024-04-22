using ConfigurationReader.Worker.Events;
using ConfigurationReader.Worker.Services;
using ConfigurationReader.Worker.Settings;
using MassTransit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RedLockNet.SERedis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationReader.Worker.Consumers
{
    public class ServiceConfigurationStorageChangedEventConsumer : IConsumer<ServiceConfigurationStorageChangedEvent>
    {
        private readonly RedisService _redisService;
        private readonly ILogger<ServiceConfigurationStorageCreatedEventConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;
        public ServiceConfigurationStorageChangedEventConsumer(RedisService redisService, ILogger<ServiceConfigurationStorageCreatedEventConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _redisService = redisService;
            _logger = logger;
            _publishEndpoint = publishEndpoint;
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
                var serilizeMessage = JsonSerializer.Serialize(@event);
                _redisService.GetDb().HashSet(@event.ApplicationName, @event.Id, serilizeMessage);
                _logger.LogInformation($"{nameof(context.Message)} is finished.");                
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(context.Message)} error occured. Error: {ex.Message}");
            }
        }
    }
}
