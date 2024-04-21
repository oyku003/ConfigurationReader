
using ConfigurationReader.Backgroud.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationReader.Backgroud.Data.Settings
{
    public class ServiceConfigurationSeed : IEntityTypeConfiguration<ServiceConfiguration>
    {
        public void Configure(EntityTypeBuilder<ServiceConfiguration> builder)
        {
            builder.HasData(new ServiceConfiguration
            {Id=1,
                Name="SiteName",
                Type="String",
                Value="Boyner.com.tr",
                IsActive=1,
                ApplicationName="SERVICE-A"

            },
            new ServiceConfiguration
            {
                Id = 2,
                Name = "IsBasktetEnabled",
                Type = "Boolean",
                Value = "1",
                IsActive = 1,
                ApplicationName = "SERVICE-B"
            },
            new ServiceConfiguration
            {
                Id = 3,
                Name = "MaxItemCount",
                Type = "Int",
                Value = "50",
                IsActive = 0,
                ApplicationName = "SERVICE-A"
            });
        }
    }
}
