using System.Text.Json;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ;
using Structures;
using XmlParser.Helpers;
using Xunit;

namespace Tests;

public class DatabaseTest : IAsyncLifetime
{
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
                options.UseSqlite("Data Source=database-unit-test.sqlite");
            })
            .AddScoped<Repository>()
            .AddSingleton<RabbitMqClient>()
            .AddSingleton<XmlParser.Microservice>()
            .AddSingleton<DataProcessor.Microservice>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

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
    public async Task DatabaseInteraction()
    {
        var jsonFilePath = Path.Combine("Resources", "status.json");
        var json = await File.ReadAllTextAsync(jsonFilePath);
        var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(json)?.RandomizeModuleState();
        
        Assert.NotNull(instrumentStatus);
        
        var moduleState = instrumentStatus.DeviceStatuses[0].RapidControlStatus.CombinedStatus?.ModuleState;
        
        Assert.NotNull(moduleState);
            
        await Repository.SaveOrUpdateInstrumentStatusAsync(instrumentStatus);

        var savedInstrumentStatusEntity = await DbContext.InstrumentStatuses
            .Include(instrumentStatusEntity => instrumentStatusEntity.DeviceStatuses)
            .ThenInclude(deviceStatusEntity => deviceStatusEntity.RapidControlStatus)
            .ThenInclude(rapidControlStatusEntity => rapidControlStatusEntity.CombinedStatus)
            .FirstOrDefaultAsync(x => x.PackageID == instrumentStatus.PackageID);
        
        Assert.NotNull(savedInstrumentStatusEntity);
            
        var savedModuleState = savedInstrumentStatusEntity.DeviceStatuses[0].RapidControlStatus.CombinedStatus.ModuleState;
        Assert.NotNull(savedModuleState);
        Assert.Equal(moduleState, savedModuleState);

        instrumentStatus.RandomizeModuleState();
        moduleState = instrumentStatus.DeviceStatuses[0].RapidControlStatus.CombinedStatus?.ModuleState;
        await Repository.SaveOrUpdateInstrumentStatusAsync(instrumentStatus);

        savedInstrumentStatusEntity = await DbContext.InstrumentStatuses
            .Include(instrumentStatusEntity => instrumentStatusEntity.DeviceStatuses)
            .ThenInclude(deviceStatusEntity => deviceStatusEntity.RapidControlStatus)
            .ThenInclude(rapidControlStatusEntity => rapidControlStatusEntity.CombinedStatus)
            .FirstOrDefaultAsync(x => x.PackageID == instrumentStatus.PackageID);
        
        Assert.NotNull(savedInstrumentStatusEntity);
            
        savedModuleState = savedInstrumentStatusEntity.DeviceStatuses[0].RapidControlStatus.CombinedStatus.ModuleState;
        Assert.NotNull(savedModuleState);
        Assert.Equal(moduleState, savedModuleState);
    }
}