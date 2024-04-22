using AutoMapper;
using ConfigurationReader.Api.Data.Entities;
using ConfigurationReader.Api.Interfaces;
using ConfigurationReader.Api.Services.Cqrs.Commands;
using ConfigurationReader.Services.Mappers;
using ConfigurationReader.Shared.Exceptions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Services.Cqrs.Handlers
{
    public class UpdateServiceConfigurationCommandHandler : IRequestHandler<UpdateServiceConfigurationCommand>
    {
        private readonly IWriteOnlyRepository<ServiceConfiguration> _repository;
        private readonly IReadOnlyRepository<ServiceConfiguration> _readRepository;

        public UpdateServiceConfigurationCommandHandler(IWriteOnlyRepository<ServiceConfiguration> repository, IReadOnlyRepository<ServiceConfiguration> readRepository)
        {
            _repository = repository;
            _readRepository = readRepository;
        }

        public async Task<Unit> Handle(UpdateServiceConfigurationCommand request, CancellationToken cancellationToken)
        {
            if (request == default)
            {
                throw new CustomException($"{nameof(request)} is not null");
            }

            if (request.Id==default)
            {
                throw new CustomException($"{nameof(request.Type)} can not be zero");
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

            var entity = (await _readRepository.Where(x => x.Id == request.Id)).FirstOrDefault();

            if (entity == default)
            {
                throw new CustomException($"{nameof(entity)} could not be found");
            }

            ObjectMapper.Mapper.Map(request, entity);
            _repository.Update(entity);

            return await Task.FromResult(Unit.Value);
        }
    }
}
