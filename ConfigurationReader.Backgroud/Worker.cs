using ConfigurationReader.Backgroud.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConfigurationReader.Backgroud
{
    public class Worker : BackgroundService
    {
        private readonly int _timerExpire;
        public IServiceProvider Services { get; }
        public Worker(int timerExpire, IServiceProvider services)
        {
            _timerExpire = timerExpire;
            Services = services;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {

            //log

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

                        await configurationReaderService.
                            SetStorage();
                    }


                    await Task.Delay(new TimeSpan(0, 0, _timerExpire), stoppingToken);
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}
