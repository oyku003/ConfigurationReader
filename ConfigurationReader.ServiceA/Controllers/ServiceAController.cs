using ConfigurationReader.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ConfigurationReader.ServiceA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceAController : ControllerBase
    {
        private readonly IConfigurationReaderService _configurationReaderService;

        public ServiceAController(IConfigurationReaderService configurationReaderService)
        {
            _configurationReaderService = configurationReaderService;
        }

        [HttpGet]
        public async Task<int> GetValue()
            => await _configurationReaderService.GetValue<int>("MaxItemCount");
           
    }
}
