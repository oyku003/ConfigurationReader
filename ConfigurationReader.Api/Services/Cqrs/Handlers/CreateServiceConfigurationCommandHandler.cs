
using ConfigurationReader.Api.Data.Entities;
using ConfigurationReader.Api.Interfaces;
using ConfigurationReader.Api.Services.Cqrs.Commands;
using ConfigurationReader.Services.Mappers;
using ConfigurationReader.Shared.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Services.Cqrs.Handlers
{
    public class CreateServiceConfigurationCommandHandler : IRequestHandler<CreateServiceConfigurationCommand>
    {
        private readonly IWriteOnlyRepository<ServiceConfiguration> _repository;
        private readonly ILogger<CreateServiceConfigurationCommandHandler> _logger;
        public CreateServiceConfigurationCommandHandler(IWriteOnlyRepository<ServiceConfiguration> repository, ILogger<CreateServiceConfigurationCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Unit> Handle(CreateServiceConfigurationCommand request, CancellationToken cancellationToken)
        {
            if (request == default)
            {
                throw new CustomException($"{nameof(request)} can not be null");
            }

            if (string.IsNullOrWhiteSpace(request.Type))
            {
                throw new CustomException($"{nameof(request.Type)} can not be null");
            }

            if (string.IsNullOrWhiteSpace(request.Value))
            {
                throw new CustomException($"{nameof(request.Value)} can not be null");
            }

            if (string.IsNullOrWhiteSpace(request.ApplicationName))
            {
                throw new CustomException($"{nameof(request.ApplicationName)} can not be null");
            }

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new CustomException($"{nameof(request.Name)} can not be null");
            }

            var entity = ObjectMapper.Mapper.Map<ServiceConfiguration>(request);
            await _repository.AddAsync(entity);
            _logger.LogInformation($"{JsonSerializer.Serialize(entity)} created");

            return await Task.FromResult(Unit.Value);
        }
    }
}
