using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ;
using XmlParser.Helpers;

namespace XmlParser
{
    public class Microservice(
        IConfiguration configuration,
        ILogger<Microservice> logger,
        RabbitMqClient rabbitMqClient
    ) : IHostedService, IDisposable
    {
        private int _publishedMessagesCount;
        private string XmlFilesDirectory { get; } = configuration["XmlParser:XmlDirectory"] ?? "Resources";
        private ILogger<Microservice> Logger { get; } = logger;
        private RabbitMqClient RabbitMqClient { get; } = rabbitMqClient;
        private Timer? LoadXmlFilesTimer { get; set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            LoadXmlFilesTimer = new Timer(_ => LoadAndProcessXmlFiles(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            LoadXmlFilesTimer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => LoadXmlFilesTimer?.Dispose();

        public int GetPublishedMessagesCount() => _publishedMessagesCount;

        private void LoadAndProcessXmlFiles()
        {
            var xmlFilesPaths = Directory.GetFiles(XmlFilesDirectory, "*.xml");

            foreach (var xmlFilePath in xmlFilesPaths)
            {
                Task.Run(async () =>
                {
                    var xml = await File.ReadAllTextAsync(xmlFilePath);
                    ProcessXml(xml);
                    Interlocked.Increment(ref _publishedMessagesCount);
                });
            }
        }

        private void ProcessXml(string xml)
        {
            var instrumentStatus = Parser.ParseInstrumentStatus(xml)?.RandomizeModuleState();
            var jsonInstrumentStatus = JsonConvert.SerializeObject(instrumentStatus);
            RabbitMqClient.PublishMessage(jsonInstrumentStatus);
        }
    }
}