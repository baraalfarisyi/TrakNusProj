using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace TrakNusLemburProj.Handler
{
    public class RabbitMQSubscriber
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQSubscriber()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "exchange_name", type: ExchangeType.Fanout);
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: "exchange_name", routingKey: "");
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
            };
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
    }
}
