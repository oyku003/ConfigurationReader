using ConfigurationReader.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConfigurationReader.ServiceB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceBController : ControllerBase
    {
        private readonly IConfigurationReaderService _configurationReaderService;

        public ServiceBController(IConfigurationReaderService configurationReaderService)
        {
            _configurationReaderService = configurationReaderService;
        }
        public async Task<int> GetValue()
            => await _configurationReaderService.GetValue<int>("MaxItemCount");
    }
}
