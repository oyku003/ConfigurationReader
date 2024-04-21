using AutoMapper;
using ConfigurationReader.Api.Data.Entities;
using ConfigurationReader.Api.Interfaces;
using ConfigurationReader.Api.Services.Cqrs.Queries;
using ConfigurationReader.Services.Mappers;
using ConfigurationReader.Shared.Models.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Services.Cqrs.Handlers
{
    public class GetServiceConfigurationQueryHandler : IRequestHandler<GetServiceConfigurationsQuery, List<ServiceConfigurationDto>>
    {
        private readonly IReadOnlyRepository<ServiceConfiguration> _repository;

        public GetServiceConfigurationQueryHandler(IReadOnlyRepository<ServiceConfiguration> repository)
        {
            _repository = repository;
        }

        public async Task<List<ServiceConfigurationDto>> Handle(GetServiceConfigurationsQuery request, CancellationToken cancellationToken)
        {
            //todo: validation
            //todo: redis olsun mu düşün
            return ObjectMapper.Mapper.Map<List<ServiceConfigurationDto>>( (await _repository.Where(x=>x.IsActive ==1)).ToList());

        }
    }
}
