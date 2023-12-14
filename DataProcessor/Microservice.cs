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
    ) 
    : IHostedService
{
    private int _receivedMessagesCount;

    public int GetReceivedMessagesCount() => _receivedMessagesCount;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        rabbitMqClient.SubscribeForMessages(ProcessMessageAsync);
        logger.LogInformation("Microservice has been started.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Microservice has been stopped.");
        return Task.CompletedTask;
    }

    private async void ProcessMessageAsync(string message)
    {
        try
        {
            Interlocked.Increment(ref _receivedMessagesCount);
            var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(message);
            if (instrumentStatus == null) return;
            await repository.SaveOrUpdateInstrumentStatusAsync(instrumentStatus);
            
            var loggingInformationMessage = $"Incoming instrument-status has been successfully deserialized " +
                                     $"& saved to database with next module states:{Environment.NewLine}";

            foreach (var device in instrumentStatus.DeviceStatuses)
            {
                loggingInformationMessage += $"[{instrumentStatus.PackageID}]" +
                                      $"[{device.ModuleCategoryID}]" +
                                      $"[{device.RapidControlStatus.CombinedStatus?.ModuleState}]" +
                                      $"{Environment.NewLine}";
            }

            logger.LogInformation("{LoggingInformationMessage}", loggingInformationMessage);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error processing incoming message.");
        }
    }
}