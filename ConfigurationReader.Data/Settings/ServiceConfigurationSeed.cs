using ConfigurationReader.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Data.Settings
{
    public class ServiceConfigurationSeed : IEntityTypeConfiguration<ServiceConfiguration>
    {
        public void Configure(EntityTypeBuilder<ServiceConfiguration> builder)
        {
            builder.HasData(new ServiceConfiguration
            {
                Name="SiteName",
                Type="String",
                Value="Boyner.com.tr",
                IsActive=1,
                ApplicationName="SERVICE-A"

            },
            new ServiceConfiguration
            {
                Name = "IsBasktetEnabled",
                Type = "Boolean",
                Value = "1",
                IsActive = 1,
                ApplicationName = "SERVICE-B"
            },
            new ServiceConfiguration
            {
                Name = "MaxItemCount",
                Type = "Int",
                Value = "50",
                IsActive = 0,
                ApplicationName = "SERVICE-A"
            });
        }
    }
}
