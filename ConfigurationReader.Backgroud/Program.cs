using ConfigurationReader.Background.Consumers;
using ConfigurationReader.Background.Data.Repositories;
using ConfigurationReader.Background.Services;
using ConfigurationReader.Background.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System;

namespace ConfigurationReader.Background
{
    public class Program
    {
        static bool? _isRunningInContainer;
        static bool IsRunningInContainer =>
            _isRunningInContainer ??= bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) && inContainer;
        private static IConfigurationRoot configuration;
        public static void Main(string[] args)
        {
            configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false)
        .Build();
            var builder = CreateHostBuilder(args);
            builder.UseSerilog(Logging.ConfigureLogging);

            builder.Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    
                    services.Configure<RedisSetting>(configuration.GetSection("RedisSettings"));
                    services.Configure<DbSetting>(configuration.GetSection("ConnectionStrings"));
                    services.AddSingleton<RedisService>(sp =>
                    {
                        var redisSettings = sp.GetRequiredService<IOptions<RedisSetting>>().Value;

                        var redis = new RedisService(redisSettings.Host, redisSettings.Port);

                        redis.Connect();

                        return redis;
                    });

                    services.AddDbContext<AppDbContext>(options =>
                    {
                        options.UseSqlServer(configuration.GetConnectionString("SqlServer"), sqlOptions =>
                        {
                             sqlOptions.MigrationsAssembly("ConfigurationReader.Background");
                        });
                    });

                    var rabbitMqSettings = configuration.GetSection("RabbitMqSettings").Get<MessageBrokerSetting>();
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<ServiceConfigurationStorageChangedEventConsumer>();
                        x.AddConsumer<ServiceConfigurationStorageCreatedEventConsumer>();
                        x.AddConsumer<ServiceConfigurationStorageDeletedEventConsumer>();
                        var host = IsRunningInContainer ?"rabbitmq":rabbitMqSettings.Url;
                        x.UsingRabbitMq((context, cfg) =>
                        {
                           
                            cfg.Host(host, rabbitMqSettings.VirtualHost, host =>
                            {
                                host.Username(rabbitMqSettings.Username);
                                host.Password(rabbitMqSettings.Password);
                            });

                            cfg.ReceiveEndpoint("ServiceConfigurationStorageChangedQueue", e =>
                            {
                                e.ConfigureConsumer<ServiceConfigurationStorageChangedEventConsumer>(context);
                            });

                            cfg.ReceiveEndpoint("ServiceConfigurationStorageCreatedQueue", e =>
                            {
                                e.ConfigureConsumer<ServiceConfigurationStorageCreatedEventConsumer>(context);
                            });
                            
                            cfg.ReceiveEndpoint("ServiceConfigurationStorageDeletedQueue", e =>
                            {
                                e.ConfigureConsumer<ServiceConfigurationStorageDeletedEventConsumer>(context);
                            });
                        });

                    });
                    services.AddMassTransitHostedService();
                    services.AddScoped<IConfigurationReaderBackground, ConfigurationReaderBackground>();
                    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                    services.AddHostedService<Worker>(x =>
                    {
                        return new Worker(Convert.ToInt32(configuration.GetSection("TimerInfoSettings")["Expire"]), x);
                    });
                });
    }
}
