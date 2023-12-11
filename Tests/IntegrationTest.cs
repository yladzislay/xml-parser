using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ;
using Xunit;

namespace Tests;

public class IntegrationTest : IAsyncLifetime
{
    private RabbitMqClient RabbitMqClient { get; set; } = null!;
    private XmlParser.Microservice XmlParser { get; set; } = null!;
    private DataProcessor.Microservice DataProcessor { get; set; } = null!;
    private DatabaseContext DbContext { get; set; } = null!;
    private Repository Repository { get; set; } = null!;

    public Task InitializeAsync()
    {
        var configuration = new ConfigurationBuilder().Build();

        var serviceCollection = new ServiceCollection()
            .AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            })
            .AddSingleton<IConfiguration>(configuration)
            .AddAutoMapper(typeof(AutoMapperProfile))
            .AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlite("Data Source=integration-full-cycle-test.sqlite");
            })
            .AddScoped<Repository>()
            .AddSingleton<RabbitMqClient>()
            .AddSingleton<XmlParser.Microservice>()
            .AddSingleton<DataProcessor.Microservice>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        RabbitMqClient = serviceProvider.GetRequiredService<RabbitMqClient>();
        XmlParser = serviceProvider.GetRequiredService<XmlParser.Microservice>();
        DataProcessor = serviceProvider.GetRequiredService<DataProcessor.Microservice>();
        DbContext = serviceProvider.GetRequiredService<DatabaseContext>();
        Repository = serviceProvider.GetRequiredService<Repository>();

        DbContext.Database.EnsureDeleted();
        DbContext.Database.EnsureCreated();
        DbContext.Database.Migrate();

        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        DbContext.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task FullCycle()
    {
        Assert.True(RabbitMqClient.IsConnected());

        await XmlParser.StartAsync(CancellationToken.None);
        await DataProcessor.StartAsync(CancellationToken.None);

        await Task.Delay(5500);

        await XmlParser.StopAsync(CancellationToken.None);
        await DataProcessor.StopAsync(CancellationToken.None);

        var publishedMessagesCount = XmlParser.GetPublishedMessagesCount();
        var processedMessagesCount = DataProcessor.GetReceivedMessagesCount();

        Assert.NotEqual(0, publishedMessagesCount);
        Assert.NotEqual(0, processedMessagesCount);
        Assert.Equal(publishedMessagesCount, processedMessagesCount);
    }
}