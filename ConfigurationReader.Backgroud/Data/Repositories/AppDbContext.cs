using ConfigurationReader.Backgroud.Data.Entities;
using ConfigurationReader.Backgroud.Data.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Backgroud.Data.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ServiceConfiguration> ServiceConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ServiceConfigurationSetting());
            modelBuilder.ApplyConfiguration(new ServiceConfigurationSeed());
            // builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
