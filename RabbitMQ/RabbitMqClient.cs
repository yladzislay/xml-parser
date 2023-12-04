using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class RabbitMqClient : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqClient(IConfiguration configuration)
        {
            var hostName = configuration["RabbitMQ:HostName"] ?? "localhost";
            var port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672");

            var connectionFactory = new ConnectionFactory { HostName = hostName, Port = port };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public bool IsConnected()
        {
            return _connection.IsOpen;
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}