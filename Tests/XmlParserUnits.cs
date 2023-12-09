using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ;
using Structures;
using Structures.CombinedStatuses;
using XmlParser;
using Xunit;

namespace Tests;

public class XmlParserUnits
{
    [Fact]
    public void InstrumentStatus_ShouldBeParsed_Successfully()
    {
        var xmlFilePath = Path.Combine("Resources", "status_1.xml");
        var xml = File.ReadAllText(xmlFilePath);
            
        var instrumentStatus = Parser.ParseInstrumentStatus(xml);

        Assert.NotNull(instrumentStatus);
        Assert.IsType<InstrumentStatus>(instrumentStatus);

        Assert.NotNull(instrumentStatus.DeviceStatuses);
        Assert.NotEmpty(instrumentStatus.DeviceStatuses);

        var firstDeviceStatus = instrumentStatus.DeviceStatuses[0];
        Assert.NotNull(firstDeviceStatus.RapidControlStatus);
        Assert.IsType<CombinedSamplerStatus>(firstDeviceStatus.RapidControlStatus.CombinedStatus);

        var secondDeviceStatus = instrumentStatus.DeviceStatuses[1];
        Assert.NotNull(secondDeviceStatus.RapidControlStatus);
        Assert.IsType<CombinedPumpStatus>(secondDeviceStatus.RapidControlStatus.CombinedStatus);

        var thirdDeviceStatus = instrumentStatus.DeviceStatuses[2];
        Assert.NotNull(thirdDeviceStatus.RapidControlStatus);
        Assert.IsType<CombinedOvenStatus>(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
    }
        
    [Fact]
    public async Task XmlFiles_ShouldBeProcessed_Successfully()
    {
        var configuration = new ConfigurationBuilder().Build();
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<Timer>(_ => new Timer(_ => { }))
            .AddSingleton<RabbitMqClient>()
            .AddSingleton<Microservice>()
            .BuildServiceProvider();
            
        var microservice = serviceProvider.GetRequiredService<Microservice>();

        await microservice.StartAsync(default);
        await Task.Delay(2000);
        await microservice.StopAsync(default);
            
        Assert.True(microservice.GetPublishedMessagesCount() > 0);
    }
}