using ConfigurationReader.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Services.Services
{
    public class ConfigurationIntegerType : IConfigurationType
    {
        public string Type => "Int";

        public object GetValue(string value)
            => TypeHelper.Parse("System.Int32", value);
    }
}
