using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConfigurationReader.Api.Data.Entities
{
    public class ServiceConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ApplicationName { get; set; }
        public sbyte IsActive { get; set; }

        public override bool Equals(System.Object obj)
        {
            if (obj == null)
                return false;

            ServiceConfiguration p = obj as ServiceConfiguration;
            if ((System.Object)p == null)
                return false;

            return (Id == p.Id) && (Name == p.Name) && (Type == p.Type);
        }

        public bool Equals(ServiceConfiguration p)
        {
            if ((object)p == null)
                return false;

            return (Id == p.Id) && (Name == p.Name) && (Type == p.Type);
        }

    }

}
