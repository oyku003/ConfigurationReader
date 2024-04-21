
using ConfigurationReader.Services.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Services.Data.Settings
{
    public class ServiceConfigurationSetting : IEntityTypeConfiguration<ServiceConfiguration>
    {
        public void Configure(EntityTypeBuilder<ServiceConfiguration> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.ApplicationName).IsRequired();
            builder.ToTable("ServiceConfiguration");
        }
    }
}
