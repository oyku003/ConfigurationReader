using MediatR;

namespace ConfigurationReader.Api.Services.Cqrs.Commands
{
    public class UpdateServiceConfigurationCommand : BaseServiceConfigurationCommand,IRequest
    {
        public int Id { get; set; }
    }
}
