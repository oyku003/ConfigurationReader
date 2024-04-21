using ConfigurationReader.Backgroud.Events;
using ConfigurationReader.Backgroud.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationReader.Backgroud.Consumers
{
    public class ServiceConfigurationStorageCreatedEventConsumer : IConsumer<ServiceConfigurationStorageCreatedEvent>
    {
        private readonly RedisService _redisService;

        public ServiceConfigurationStorageCreatedEventConsumer(RedisService redisService)
        {
            _redisService = redisService;
        }
        public async Task Consume(ConsumeContext<ServiceConfigurationStorageCreatedEvent> context)
        {
            if (context.Message == default)
            {
                return;
            }

            try
            {
                var @event = context.Message;
                _redisService.GetDb().HashSet(@event.ApplicationName, @event.Id, JsonSerializer.Serialize(@event));
            }
            catch (Exception ex)
            {
                //log
            }
        }
    }
}
