﻿using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    public class RabbitMqClient : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string QueueName = "InstrumentStatuses";

        public RabbitMqClient(IConfiguration configuration)
        {
            var hostName = configuration["RabbitMQ:HostName"] ?? "localhost";
            var port = int.Parse(configuration["RabbitMQ:Port"] ?? "5672");

            var connectionFactory = new ConnectionFactory { HostName = hostName, Port = port };
            _connection = connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, autoDelete: true);
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
        }
        
        public void SubscribeForMessages(Action<string> messageHandler)
        {
            var consumer = new EventingBasicConsumer(_channel);
            
            consumer.Received += (model, basicDeliverEventArgs) =>
            {
                var message = Encoding.UTF8.GetString(basicDeliverEventArgs.Body.ToArray());
                messageHandler.Invoke(message);
            };

            _channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);
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