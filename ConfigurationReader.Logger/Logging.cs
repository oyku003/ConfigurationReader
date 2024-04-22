using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using System;

namespace ConfigurationReader.Logger
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogging => (builderContext, loggerConfiguration) =>
        {
            var environment = builderContext.HostingEnvironment;

            loggerConfiguration
            .ReadFrom.Configuration(builderContext.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("Env", environment.EnvironmentName)
            .Enrich.WithProperty("AppName", environment.ApplicationName);

            var elasticSearchBaseUrl = builderContext.Configuration.GetSection("Elasticsearch")["BaseUrl"];
            var elasticSearchUserName = builderContext.Configuration.GetSection("Elasticsearch")["UserName"];
            var elasticSearchPassword = builderContext.Configuration.GetSection("Elasticsearch")["Password"];
            var indexName = builderContext.Configuration.GetSection("Elasticsearch")["IndexName"];

            loggerConfiguration.WriteTo.Elasticsearch(new(new Uri(elasticSearchBaseUrl))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = Serilog.Sinks.Elasticsearch.AutoRegisterTemplateVersion.ESv8,
                IndexFormat = $"{indexName}-{environment.EnvironmentName}-logs-" + "{0:yyy.MM.dd}",
                ModifyConnectionSettings = x => x.BasicAuthentication(elasticSearchUserName, elasticSearchPassword),
                CustomFormatter = new ElasticsearchJsonFormatter()
            });
        };


       
    }
}

