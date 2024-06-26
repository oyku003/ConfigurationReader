﻿using ConfigurationReader.Worker.Data.Entities;
using ConfigurationReader.Worker.Data.Repositories;
using ConfigurationReader.Worker.Events;
using MassTransit;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConfigurationReader.Worker.Services
{
    public class ConfigurationReaderBackground : IConfigurationReaderBackground
    {
        private readonly RedisService _redisService;
        private readonly IRepository<ServiceConfiguration> _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        public ConfigurationReaderBackground(RedisService redisService, IRepository<ServiceConfiguration> repository, IPublishEndpoint publishEndpoint)
        {
            _redisService = redisService;
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }
        public async Task SetStorage()        
        {
            var storageConfigurations = (await _repository.Where(x => x.IsActive == 1)).ToList();

            if (!storageConfigurations.Any())
            {
                var deletedStorageConfigurations = (await _repository.Where(x => x.IsActive == 0)).ToList();

                deletedStorageConfigurations?.ForEach(deletedStorageConfiguration =>
                {
                    _publishEndpoint.Publish<ServiceConfigurationStorageDeletedEvent>(new ServiceConfigurationStorageDeletedEvent { ApplicationName = deletedStorageConfiguration.ApplicationName, Id= deletedStorageConfiguration.Id });
                });
            }

            var applicationNames = storageConfigurations.GroupBy(x => x.ApplicationName).Select(x => x.Key).ToList();

            applicationNames.ForEach(applicationName =>
            {
                var redisStorages = _redisService.GetDb().HashGetAll(applicationName);
                var storages = storageConfigurations.Where(x => x.ApplicationName == applicationName).ToList();

                var deserilizeRedisStorages = redisStorages != default
                                                            ? redisStorages.Select(d => JsonSerializer.Deserialize<ServiceConfiguration>(d.Value)).ToList()
                                                            : default;
              
                if (!deserilizeRedisStorages.Any())
                {
                    storages.ForEach(storage =>
                    {
                        _publishEndpoint.Publish<ServiceConfigurationStorageCreatedEvent>(storage);
                    });

                    return;
                }

                storages.ForEach(storage =>
                {
                    var serviceConfiguration = deserilizeRedisStorages.Where(x => x.Id == storage.Id).First();

                    if (serviceConfiguration == null)
                    {
                        _publishEndpoint.Publish<ServiceConfigurationStorageCreatedEvent>(serviceConfiguration);
                    }
                    else if(!storage.Equals(serviceConfiguration))
                    {
                        _publishEndpoint.Publish<ServiceConfigurationStorageChangedEvent>(serviceConfiguration);
                    }
                });

            });
        }
    }
}
