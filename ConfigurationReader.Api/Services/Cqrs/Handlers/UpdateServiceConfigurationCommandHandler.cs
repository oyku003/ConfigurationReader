using AutoMapper;
using ConfigurationReader.Api.Data.Entities;
using ConfigurationReader.Api.Interfaces;
using ConfigurationReader.Api.Services.Cqrs.Commands;
using ConfigurationReader.Services.Mappers;
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
            //todo: validation
            //todo: redis olsun mu düşün
           var entity = (await _readRepository.Where(x => x.Id == request.Id)).FirstOrDefault();

            if (entity == default)
            {
                //todo
            }

            ObjectMapper.Mapper.Map(request, entity);
            _repository.Update(entity);

            return await Task.FromResult(Unit.Value);
        }
    }
}
