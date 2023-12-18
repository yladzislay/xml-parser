﻿using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ;

namespace Executor;

public static class Executor
{
    public static async Task Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                services.AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                    builder.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning);
                });
                services.AddSingleton(context.Configuration);
                services.AddAutoMapper(typeof(AutoMapperProfile));
                services.AddDbContext<DatabaseContext>(options =>
                    options.UseSqlite(context.Configuration.GetConnectionString("InstrumentStatusesDb")));
                services.AddScoped<Repository>();
                services.AddSingleton<RabbitMqClient>();
                services.AddSingleton<IHostedService, XmlParser.Microservice>();
                services.AddSingleton<IHostedService, DataProcessor.Microservice>();
            })
            .Build();

        using var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        await dbContext.Database.EnsureCreatedAsync();

        await host.RunAsync();
    }
}
