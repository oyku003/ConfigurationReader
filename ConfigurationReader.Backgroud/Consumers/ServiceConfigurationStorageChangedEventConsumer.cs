using ConfigurationReader.Backgroud.Events;
using ConfigurationReader.Backgroud.Services;
using MassTransit;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static StackExchange.Redis.Role;

namespace ConfigurationReader.Backgroud.Consumers
{
    public class ServiceConfigurationStorageChangedEventConsumer : IConsumer<ServiceConfigurationStorageChangedEvent>
    {
        private readonly RedisService _redisService;

        public ServiceConfigurationStorageChangedEventConsumer(RedisService redisService)
        {
            _redisService = redisService;     
        }
        public async Task Consume(ConsumeContext<ServiceConfigurationStorageChangedEvent> context)
        {
            if (context.Message == default)
            {
                return;
            }

            try
            {
                var @event= context.Message;
                _redisService.GetDb().HashDelete(@event.ApplicationName, @event.Id);
                _redisService.GetDb().HashSet(@event.ApplicationName, @event.Id, JsonSerializer.Serialize(@event));
            }
            catch (Exception ex)
            {
                //log
            }
        }
    }
}
