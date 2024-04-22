using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigurationReader.Shared.Models.Dtos;

namespace ConfigurationReader.Shared.Models.Requests
{
    public class UpdateServiceConfigurationRequest : BaseServiceConfigurationDto
    {
        public int Id { get; set; }
    }
}
