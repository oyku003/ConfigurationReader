using ConfigurationReader.Api.Data.Entities;
using ConfigurationReader.Api.Interfaces;
using ConfigurationReader.Api.Services.Cqrs.Queries;
using ConfigurationReader.Services.Mappers;
using ConfigurationReader.Shared.Exceptions;
using ConfigurationReader.Shared.Models.Dtos;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Services.Cqrs.Handlers
{
    public class GetServiceConfigurationByIdQueryHandler : IRequestHandler<GetServiceConfigurationByIdQuery, ServiceConfigurationDto>
    {
        private readonly IReadOnlyRepository<ServiceConfiguration> _repository;

        public GetServiceConfigurationByIdQueryHandler(IReadOnlyRepository<ServiceConfiguration> repository)
        {
            _repository = repository;
        }
        public async Task<ServiceConfigurationDto> Handle(GetServiceConfigurationByIdQuery request, CancellationToken cancellationToken)
        {
            if (request== default)
            {
                throw new CustomException($"{nameof(request)} can not be null");
            }

            var result = (await _repository.Where(x => x.Id == request.Id && x.IsActive == 1)).FirstOrDefault();

            if (result == default)
            {
                throw new CustomException($"{nameof(result)} could not be found");
            }

            return ObjectMapper.Mapper.Map<ServiceConfigurationDto>(result);
        }
    }
}
