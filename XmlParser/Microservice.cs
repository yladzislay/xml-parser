using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ;
using XmlParser.Helpers;

namespace XmlParser;

public class Microservice
    (
        IConfiguration configuration,
        ILogger<Microservice> logger,
        RabbitMqClient rabbitMqClient
    ) 
    : IHostedService, IDisposable
{
    private int _publishedMessagesCount;
    private string XmlFilesDirectory { get; } = configuration["XmlParser:XmlDirectory"] ?? "Resources";
    private ILogger<Microservice> Logger { get; } = logger;
    private RabbitMqClient RabbitMqClient { get; } = rabbitMqClient;
    private Timer? LoadXmlFilesTimer { get; set; }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        LoadXmlFilesTimer = new Timer(_ => LoadAndProcessXmlFiles(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        Logger.LogInformation("Microservice has started.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        LoadXmlFilesTimer?.Change(Timeout.Infinite, 0);
        Logger.LogInformation("Microservice has stopped.");
        return Task.CompletedTask;
    }

    public void Dispose() => LoadXmlFilesTimer?.Dispose();

    public int GetPublishedMessagesCount() => _publishedMessagesCount;

    private void LoadAndProcessXmlFiles()
    {
        var xmlFilesPaths = Directory.GetFiles(XmlFilesDirectory, "*.xml");
        if (xmlFilesPaths.Length == 0)
        {
            Logger.LogWarning("No XML files found in the setup directory [{XmlFilesDirectory}].", XmlFilesDirectory);
            return;
        }
        foreach (var xmlFilePath in xmlFilesPaths) Task.Run(() => ProcessXmlFileAsync(xmlFilePath));
    }

    private async Task ProcessXmlFileAsync(string xmlFilePath)
    {
        var fileName = xmlFilePath.TrimEnd('/');
        
        try
        {
            var xml = await File.ReadAllTextAsync(xmlFilePath);
            Logger.LogInformation("The {FileName} has loaded.", fileName);
            var instrumentStatus = Parser.ParseInstrumentStatus(xml)?.RandomizeModuleState();
            var jsonInstrumentStatus = JsonSerializer.Serialize(instrumentStatus);
            RabbitMqClient.PublishMessage(jsonInstrumentStatus);
            Logger.LogInformation("The {FileName} has published.", fileName);
            Interlocked.Increment(ref _publishedMessagesCount);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "Error processing XML file [{FileName}].", fileName);
        }
    }
}