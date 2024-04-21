using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Shared.Models
{
    public class UpdateServiceConfigurationRequest: BaseServiceConfiguration
    {
        public int Id { get; set; }
    }
}
