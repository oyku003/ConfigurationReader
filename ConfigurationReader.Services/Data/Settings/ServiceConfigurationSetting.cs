
using ConfigurationReader.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationReader.Data.Settings
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
            builder.Property(x => x.IsActive).IsRequired();
            builder.ToTable("ServiceConfiguration");
        }
    }
}
