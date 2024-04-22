using AutoMapper;
using ConfigurationReader.Shared.Models.Dtos;
using ConfigurationReader.Shared.Models.Requests;
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
