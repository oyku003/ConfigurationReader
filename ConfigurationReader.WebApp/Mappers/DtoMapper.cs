using AutoMapper;
using ConfigurationReader.Shared.Models;
using ConfigurationReader.Shared.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.WebApp.Mappers
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ServiceConfigurationDto, CreateServiceConfigurationRequest>();
            CreateMap<ServiceConfigurationDto, UpdateServiceConfigurationRequest>();
        }

    }
}
