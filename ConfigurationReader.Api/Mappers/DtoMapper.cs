using AutoMapper;
using ConfigurationReader.Api.Data.Entities;
using ConfigurationReader.Api.Services.Cqrs.Commands;
using ConfigurationReader.Api.Services.Cqrs.Queries;
using ConfigurationReader.Shared.Models.Dtos;
using ConfigurationReader.Shared.Models.Requests;
using ConfigurationReader.Shared.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Services.Mappers
{
    public class DtoMapper:Profile
    {
        public DtoMapper()
        {
            CreateMap<CreateServiceConfigurationCommand, ServiceConfiguration>()
                    .ForMember(dest => dest.IsActive, from => from.MapFrom(s => 1));
            
            CreateMap<UpdateServiceConfigurationCommand, ServiceConfiguration>()
                .ForMember(dest=>dest.IsActive, from=> from.Ignore());            
            CreateMap<CreateServiceConfigurationRequest,CreateServiceConfigurationCommand>();
            CreateMap<UpdateServiceConfigurationRequest,UpdateServiceConfigurationCommand>();
            CreateMap<ServiceConfigurationDto, GetServiceConfigurationResponse>();
            CreateMap<ServiceConfigurationDto, ServiceConfiguration>().ReverseMap();
                       
        }
        
    }
}
