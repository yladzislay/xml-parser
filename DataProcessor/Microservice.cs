using System.Text.Json;
using Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ;
using Structures;

namespace DataProcessor;

public class Microservice
    (
        ILogger<Microservice> logger,
        RabbitMqClient rabbitMqClient, 
        Repository repository
    ) : IHostedService
{
    private int ReceivedMessagesCount { get; set; }
    public int GetReceivedMessagesCount() => ReceivedMessagesCount;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("DataProcessorMicroservice is starting.");
        rabbitMqClient.SubscribeForMessages(ProcessMessageAsync);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("DataProcessorMicroservice is stopping.");
        return Task.CompletedTask;
    }

    private async void ProcessMessageAsync(string message)
    {
        ReceivedMessagesCount++;
        var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(message);
        if (instrumentStatus == null) return;
        await repository.SaveOrUpdateInstrumentStatusAsync(instrumentStatus);
    }
}