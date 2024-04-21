using ConfigurationReader.Backgroud.Consumers;
using ConfigurationReader.Backgroud.Data.Repositories;
using ConfigurationReader.Backgroud.Services;
using ConfigurationReader.Backgroud.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace ConfigurationReader.Backgroud
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
             CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    
                    services.Configure<RedisSetting>(configuration.GetSection("RedisSettings"));
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
                             sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
                        });
                    });

                    var rabbitMqSettings = configuration.GetSection("RabbitMqSettings").Get<MessageBrokerSetting>();
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<ServiceConfigurationStorageChangedEventConsumer>();
                        x.AddConsumer<ServiceConfigurationStorageCreatedEventConsumer>();
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
