using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ;
using Xunit;

namespace Tests
{
    public class RabbitMqUnits : IAsyncLifetime
    {
        private IServiceProvider? _serviceProvider;
        private RabbitMqClient? RabbitMqClient { get; set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationBuilder().Build();
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddSingleton<RabbitMqClient>()
                .BuildServiceProvider();

            RabbitMqClient = _serviceProvider.GetRequiredService<RabbitMqClient>();
            await Task.CompletedTask;
        }

        public async Task DisposeAsync()
        {
            (_serviceProvider as IDisposable)?.Dispose();
            await Task.CompletedTask;
        }

        [Fact]
        public void RabbitMqClient_ShouldConnectSuccessfully()
        {
            Assert.True(RabbitMqClient?.IsConnected());
        }

        [Fact]
        public async Task RabbitMqClient_ShouldPublishMessageSuccessfully()
        {
            const string message = "test_message";
            Assert.True(RabbitMqClient?.IsConnected());
            string? receivedMessage = null;
            RabbitMqClient?.SubscribeForMessages(msg => receivedMessage = msg);
            RabbitMqClient?.PublishMessage(message);
            await Task.Delay(100);
            Assert.Equal(message, receivedMessage);
        }
    }
}