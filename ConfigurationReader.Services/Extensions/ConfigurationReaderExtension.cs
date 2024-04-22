
using ConfigurationReader.Data;
using ConfigurationReader.Data.Repositories;
using ConfigurationReader.Interfaces;
using ConfigurationReader.Services;
using ConfigurationReader.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace ConfigurationReader.Extensions
{
    public static class ConfigurationReaderExtension
    {      

        public static void AddConfigurationReaderExt(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
            services.AddScoped(typeof(IWriteOnlyRepository<>), typeof(WriteOnlyRepository<>));
            services.AddScoped<IConfigurationType, ConfigurationIntegerType>();
            services.AddScoped<IConfigurationType, ConfigurationStringType>();
            services.AddScoped<IConfigurationType, ConfigurationBooleanType>();
            services.AddScoped<IConfigurationType, ConfigurationDoubleType>();
          
            services.Configure<ApplicationInfoSetting>(configuration.GetSection("ApplicationInfoSettings"));
            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
            services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

                var redis = new RedisService(redisSettings.Host, redisSettings.Port);

                redis.Connect();

                return redis;
            });

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("ConfigurationReader");
                });
            });           

            services.AddScoped<IConfigurationReaderService, ConfigurationReaderService>();

           
            services.AddAutoMapper(typeof(Assembly));

        }
    }
}
