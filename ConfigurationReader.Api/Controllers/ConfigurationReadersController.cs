
using ConfigurationReader.Api.Services.Cqrs.Commands;
using ConfigurationReader.Api.Services.Cqrs.Queries;
using ConfigurationReader.Services.Mappers;
using ConfigurationReader.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationReader.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationReadersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ConfigurationReadersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<GetServiceConfigurationResponse>> GetAsync()
       => ObjectMapper.Mapper.Map<List<GetServiceConfigurationResponse>>(await _mediator.Send(new GetServiceConfigurationsQuery { }));

        [HttpGet("{id}")]
        public async Task<GetServiceConfigurationResponse> GetByIdAsync(int id)
            => ObjectMapper.Mapper.Map<GetServiceConfigurationResponse>(await _mediator.Send(new GetServiceConfigurationByIdQuery { Id = id })); 

        [HttpPost]
        public async Task CreateAsync(CreateServiceConfigurationRequest createServiceConfigurationRequest)
            => await _mediator.Send(ObjectMapper.Mapper.Map<CreateServiceConfigurationCommand>(createServiceConfigurationRequest));

        [HttpPut]
        public async Task UpdateAsync(UpdateServiceConfigurationRequest createServiceConfigurationRequest)
            => await _mediator.Send(ObjectMapper.Mapper.Map<UpdateServiceConfigurationCommand>(createServiceConfigurationRequest));

        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
            => await _mediator.Send(await _mediator.Send(new DeleteServiceConfigurationCommand { Id = id }));

    }
}
