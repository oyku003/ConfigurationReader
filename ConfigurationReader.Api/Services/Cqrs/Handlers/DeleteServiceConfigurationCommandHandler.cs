using ConfigurationReader.Api.Data.Entities;
using ConfigurationReader.Api.Interfaces;
using ConfigurationReader.Api.Services.Cqrs.Commands;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Services.Cqrs.Handlers
{
    public class DeleteServiceConfigurationCommandHandler : IRequestHandler<DeleteServiceConfigurationCommand>
    {
        private readonly IWriteOnlyRepository<ServiceConfiguration> _repository;
        private readonly IReadOnlyRepository<ServiceConfiguration> _readRepository;

        public DeleteServiceConfigurationCommandHandler(IWriteOnlyRepository<ServiceConfiguration> repository, IReadOnlyRepository<ServiceConfiguration> readRepository)
        {
            _repository = repository;
            _readRepository = readRepository;
        }
        public async Task<Unit> Handle(DeleteServiceConfigurationCommand request, CancellationToken cancellationToken)
        {
            var entity = (await _readRepository.Where(x => x.Id == request.Id)).FirstOrDefault();

            if (entity == default)
            {
                //todo handle
            }
            entity.IsActive = 0;
            _repository.Update(entity);

            return await Task.FromResult(Unit.Value);
        }
    }
}
