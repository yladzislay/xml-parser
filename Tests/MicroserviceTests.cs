using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XmlParser;

namespace Tests
{
    public class MicroserviceTests
    {
        [Fact]
        public async Task MicroserviceProcessesXmlFiles()
        {
            var configuration = new ConfigurationBuilder().Build();
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<Timer>(_ => new Timer(_ => { }))
                .AddSingleton<Microservice>()
                .BuildServiceProvider();
            var microservice = serviceProvider.GetRequiredService<Microservice>();

            await microservice.StartAsync(default);
            await Task.Delay(2000);
            await microservice.StopAsync(default);
            
            Assert.True(microservice.GetProcessedFilesCount() > 0);
        }
    }
}