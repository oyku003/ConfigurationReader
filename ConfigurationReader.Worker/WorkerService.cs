using ConfigurationReader.Worker.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReader.Worker
{
    public class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly int _timerExpire;
        public IServiceProvider Services { get; }
        public WorkerService(int timerExpire, IServiceProvider services)
        {
            _timerExpire = timerExpire;
            Services = services;
            _logger = services.GetRequiredService<ILogger<WorkerService>>();
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ConfigurationReader Background job is started.");

            await ExecuteAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    using (var scope = Services.CreateScope())
                    {
                        var configurationReaderService =
                            scope.ServiceProvider
                                .GetRequiredService<IConfigurationReaderBackground>();

                        await configurationReaderService.SetStorage();
                    }


                    await Task.Delay(new TimeSpan(0, 0, _timerExpire), stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"ConfigurationReader Background job is error. Error: {ex.Message}");
            }

            _logger.LogInformation("ConfigurationReader Background job is finished.");
        }
    }
}

