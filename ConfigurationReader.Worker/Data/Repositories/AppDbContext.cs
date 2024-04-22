using ConfigurationReader.Worker.Data.Entities;
using ConfigurationReader.Worker.Data.Settings;
using ConfigurationReader.Worker.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConfigurationReader.Worker.Data.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public class AppDbContextDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
        {
            private readonly string _dbConnectionString;
            public AppDbContextDbContextFactory(Microsoft.Extensions.Options.IOptions<DbSetting> dbSettingOption)
            {
                _dbConnectionString = dbSettingOption.Value.SqlServer;
            }
            public AppDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(_dbConnectionString);

                return new AppDbContext(optionsBuilder.Options);
            }
        }
        public DbSet<ServiceConfiguration> ServiceConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ServiceConfigurationSetting());
            modelBuilder.ApplyConfiguration(new ServiceConfigurationSeed());
            base.OnModelCreating(modelBuilder);
        }
    }
}
