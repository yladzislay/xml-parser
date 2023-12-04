using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ;
using Xunit;

namespace Tests;

public class RabbitMq
{
    [Fact]
    public void RabbitMqClient_ShouldConnectSuccessfully()
    {
        var configuration = new ConfigurationBuilder().Build();
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(configuration)
            .AddSingleton<RabbitMqClient>()
            .BuildServiceProvider();
        var rabbitMqClient = serviceProvider.GetRequiredService<RabbitMqClient>();
        Assert.True(rabbitMqClient.IsConnected());
    }
}