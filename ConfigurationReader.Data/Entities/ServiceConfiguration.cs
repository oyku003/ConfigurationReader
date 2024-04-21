using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Data.Entities
{
    public class ServiceConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }
        public sbyte IsActive { get; set; }
    }
}
