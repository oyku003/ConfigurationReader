using ConfigurationReader.Shared.Models.Dtos;
using MediatR;

namespace ConfigurationReader.Api.Services.Cqrs.Queries
{
    public class GetServiceConfigurationByIdQuery:IRequest<ServiceConfigurationDto>
    {
        public int Id { get; set; }
    }
}
