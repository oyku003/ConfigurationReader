using MediatR;

namespace ConfigurationReader.Api.Services.Cqrs.Commands
{
    public class DeleteServiceConfigurationCommand:IRequest
    {
        public int Id { get; set; }
    }
}
