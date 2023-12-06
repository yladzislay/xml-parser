using System.Text.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ;
using Structures;

namespace DataProcessor;

public class Microservice(ILogger<Microservice> logger, RabbitMqClient rabbitMqClient) : IHostedService
{
    private int ReceivedMessagesCount { get; set; }
    public int GetReceivedMessagesCount() => ReceivedMessagesCount;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("DataProcessorMicroservice is starting.");
        rabbitMqClient.SubscribeForMessages(ProcessMessage);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("DataProcessorMicroservice is stopping.");
        return Task.CompletedTask;
    }

    private void ProcessMessage(string message)
    {
        var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(message);
        ReceivedMessagesCount++;
    }
}