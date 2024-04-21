
using ConfigurationReader.Shared.Models.Dtos;
using MediatR;
using System.Collections.Generic;

namespace ConfigurationReader.Api.Services.Cqrs.Queries
{
    public class GetServiceConfigurationsQuery :IRequest<List<ServiceConfigurationDto>> 
    {
    }
}
