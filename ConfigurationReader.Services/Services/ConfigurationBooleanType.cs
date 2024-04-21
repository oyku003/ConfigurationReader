using ConfigurationReader.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Services.Services
{
    public class ConfigurationBooleanType : IConfigurationType
    {
        public string Type => "Boolean";

        public object GetValue(string value)
            => TypeHelper.Parse("System.Boolean", value);
    }
}
