using ConfigurationReader.Data.Entities;
using ConfigurationReader.Data.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationReader.Data
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
        }
    }
}
