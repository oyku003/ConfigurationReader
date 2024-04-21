using ConfigurationReader.Shared.Models;
using ConfigurationReader.Shared.Models.Dtos;
using ConfigurationReader.WebApp.Mappers;
using ConfigurationReader.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConfigurationReader.WebApp.Controllers
{
    public class ServiceConfigurationController : Controller
    {
        private readonly ServiceConfigurationService _serviceConfigurationService;

        public ServiceConfigurationController(ServiceConfigurationService serviceConfigurationService)
        {
            _serviceConfigurationService =serviceConfigurationService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _serviceConfigurationService.GetAsync();
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var response = await _serviceConfigurationService.GetAsync();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceConfigurationDto serviceConfigurationDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await _serviceConfigurationService.CreateAsync(ObjectMapper.Mapper.Map<CreateServiceConfigurationRequest>(serviceConfigurationDto));

            return RedirectToAction(nameof(Index));           
        }


        public async Task<IActionResult> Update(int id)
        {
            var entity = await _serviceConfigurationService.GetByIdAsync(id);

            return View(entity);

        }
        [HttpPost]
        public async Task<IActionResult> Update(ServiceConfigurationDto serviceConfigurationDto)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceConfigurationDto);              

            }
            await _serviceConfigurationService.UpdateAsync(ObjectMapper.Mapper.Map<UpdateServiceConfigurationRequest>(serviceConfigurationDto));

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceConfigurationService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
