using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using XmlParser.Helpers;

namespace XmlParser
{
    public class Microservice(
        IConfiguration configuration,
        ILogger<Microservice> logger
    ) : IHostedService, IDisposable
    {
        private string XmlFilesDirectory { get; } = configuration["XmlParser:XmlDirectory"] ?? "Resources";
        private ILogger<Microservice> Logger { get; } = logger;
        private Timer LoadXmlFilesTimer { get; set; }
        private int ProcessedFilesCount { get; set; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            LoadXmlFilesTimer = new Timer(async _ => await LoadXmlFilesAsync(), null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            LoadXmlFilesTimer.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => LoadXmlFilesTimer.Dispose();

        public int GetProcessedFilesCount() => ProcessedFilesCount;

        private async Task LoadXmlFilesAsync()
        {
            try
            {
                var xmlFilesPaths = Directory.GetFiles(XmlFilesDirectory, "*.xml");
                var processingTasks = xmlFilesPaths.Select(ProcessXmlFileAsync).ToList();
                await Task.WhenAll(processingTasks);
            }
            catch (Exception exception)
            {
                Logger.LogError("Error loading XML files: {ExceptionMessage}", exception.Message);
            }
        }

        private async Task ProcessXmlFileAsync(string xmlFilePath)
        {
            try
            {
                var xml = await File.ReadAllTextAsync(xmlFilePath);
                var instrumentStatus = Parser.ParseInstrumentStatus(xml);
                await Task.Run(() => instrumentStatus.RandomizeModuleState());
                ProcessedFilesCount++;
                var jsonInstrumentStatus = JsonConvert.SerializeObject(instrumentStatus);
            }
            catch (Exception exception)
            {
                Logger.LogError("Error processing XML file \'{XmlFilePath}\': {ExceptionMessage}", xmlFilePath, exception.Message);
            }
        }
    }
}