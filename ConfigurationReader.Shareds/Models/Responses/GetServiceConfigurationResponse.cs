﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationReader.Shared.Models.Dtos;

namespace ConfigurationReader.Shared.Models.Responses
{
    public class GetServiceConfigurationResponse : BaseServiceConfigurationDto
    {
        public int Id { get; set; }

    }
}
