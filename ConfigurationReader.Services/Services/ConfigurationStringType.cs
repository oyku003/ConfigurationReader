using ConfigurationReader.Services.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Services.Services
{
    public class ConfigurationStringType : IConfigurationType
    {
        public string Type => "String";
        public object GetValue(string value)
            => TypeHelper.Parse("System.String", value);        
    }
}
