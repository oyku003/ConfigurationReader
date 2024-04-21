using ConfigurationReader.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Services.Services
{
    public class ConfigurationDoubleType : IConfigurationType
    {
        public string Type => "Double";//todo enum or const

        public object GetValue(string value)
            => TypeHelper.Parse("System.Double", value);
    }
}
