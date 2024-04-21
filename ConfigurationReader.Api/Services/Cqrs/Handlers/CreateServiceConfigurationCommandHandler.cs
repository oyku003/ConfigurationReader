
using ConfigurationReader.Api.Data.Entities;
using ConfigurationReader.Api.Interfaces;
using ConfigurationReader.Api.Services.Cqrs.Commands;
using ConfigurationReader.Services.Mappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Services.Cqrs.Handlers
{
    public class CreateServiceConfigurationCommandHandler : IRequestHandler<CreateServiceConfigurationCommand>
    {
        private readonly IWriteOnlyRepository<ServiceConfiguration> _repository;

        public CreateServiceConfigurationCommandHandler(IWriteOnlyRepository<ServiceConfiguration> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateServiceConfigurationCommand request, CancellationToken cancellationToken)
        {
            //todo: validation
            //todo: redis olsun mu düşün
            var b = ObjectMapper.Mapper.Map<ServiceConfiguration>(request);
           await _repository.AddAsync(b);

            return await Task.FromResult(Unit.Value);
        }
    }
}
