using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ;
using Xunit;

// ReSharper disable NullableWarningSuppressionIsUsed

namespace Tests
{
    public class Integrations : IAsyncLifetime
    {
        private RabbitMqClient? RabbitMqClient { get; set; }
        private XmlParser.Microservice XmlParser { get; set; } = null!;
        private DataProcessor.Microservice DataProcessor { get; set; } = null!;

        public Task InitializeAsync()
        {
            var configuration = new ConfigurationBuilder().Build();

            var serviceCollection = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddLogging(builder =>
                {
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .AddSingleton<RabbitMqClient>()
                .AddSingleton<XmlParser.Microservice>()
                .AddSingleton<DataProcessor.Microservice>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            RabbitMqClient = serviceProvider.GetRequiredService<RabbitMqClient>();
            XmlParser = serviceProvider.GetRequiredService<XmlParser.Microservice>();
            DataProcessor = serviceProvider.GetRequiredService<DataProcessor.Microservice>();

            return Task.CompletedTask;
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
        
        [Fact]
        public async Task FullCycle()
        {
            Assert.True(RabbitMqClient?.IsConnected());

            await XmlParser.StartAsync(CancellationToken.None);
            await DataProcessor.StartAsync(CancellationToken.None);

            await Task.Delay(3000);

            await XmlParser.StopAsync(CancellationToken.None);
            await DataProcessor.StopAsync(CancellationToken.None);

            var publishedMessagesCount = XmlParser.GetPublishedMessagesCount();
            var processedMessagesCount = DataProcessor.GetReceivedMessagesCount();

            Assert.NotEqual(0, publishedMessagesCount);
            Assert.NotEqual(0, processedMessagesCount);
            Assert.Equal(publishedMessagesCount, processedMessagesCount);
        }
    }
}
