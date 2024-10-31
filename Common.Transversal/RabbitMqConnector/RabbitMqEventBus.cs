using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Common.Transversal.RabbitMqConnector
{
    public class RabbitMqEventBus : IEventBus, IDisposable
    {
        private readonly RabbitMqConfig _config;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMqEventBus(RabbitMqConfig config)
        {
            _config = config;
            var factory = new ConnectionFactory
            {
                HostName = _config.HostName,
                UserName = _config.UserName,
                Password = _config.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: _config.Exchange, type: ExchangeType.Fanout);
        }

        public void Publish<TEvent>(TEvent @event)
        {
            var routingKey = typeof(TEvent).Name;
            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));

            _channel.BasicPublish(
                exchange: _config.Exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: messageBody);
        }

        public async Task PublishAsync<TEvent>(TEvent @event)
        {
            var routingKey = typeof(TEvent).Name;
            var messageBody = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));

            _channel.BasicPublish(
                exchange: _config.Exchange,
                routingKey: routingKey,
                basicProperties: null,
                body: messageBody);

            await Task.CompletedTask;
        }

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            var routingKey = typeof(TEvent).Name;
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: _config.Exchange, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<TEvent>(message);
                handler(@event);
            };
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        public async Task SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
        {
            var routingKey = typeof(TEvent).Name;
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: _config.Exchange, routingKey: routingKey);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var @event = JsonSerializer.Deserialize<TEvent>(message);
                handler(@event);
            };
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
