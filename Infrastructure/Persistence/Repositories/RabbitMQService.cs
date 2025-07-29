using Domain.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServicesAbstractions.Messaging;
using System.Text;

namespace Persistence.Repositories
{
    public sealed class RabbitMQService : IMessagingService
    {
        
        private readonly ILogger<RabbitMQService> _logger;
        public RabbitMQService(ILogger<RabbitMQService> logger)
        {
            _logger = logger;
        }
        public async Task PublishAsync<T>(string queueName, T message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = await factory.CreateConnectionAsync())
            using(var channel = await connection.CreateChannelAsync())
            {

                await channel.QueueDeclareAsync(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                await channel.BasicPublishAsync("", queueName, body);
            }
        }

        public async Task SubscribeAsync<T>(string queueName, Func<T, Task> handler)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();
             await channel.QueueDeclareAsync(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var deserializedMessage = JsonConvert.DeserializeObject<T>(message);
                
                if (deserializedMessage != null)
                {
                    await handler(deserializedMessage);
                }
            };
            await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);
        }
    }
}
