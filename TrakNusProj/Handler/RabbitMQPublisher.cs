using RabbitMQ.Client;
using System.Text;

namespace TrakNusProj.Handler
{
    public class RabbitMQPublisher
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQPublisher()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "exchange_name", type: ExchangeType.Fanout);
        }

        public void PublishMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "exchange_name", routingKey: "", basicProperties: null, body: body);
        }
    }
}

