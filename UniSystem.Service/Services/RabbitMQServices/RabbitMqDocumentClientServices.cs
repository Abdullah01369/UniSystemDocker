using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace UniSystem.Service.Services.RabbitMQServices
{

    public class RabbitMqDocumentClientServices : IDisposable
    {
        private readonly ILogger _logger;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public static string ExchangeName = "DocumentExchange";
        public static string RoutingDoc = "document-route";
        public static string QueueName = "queue-document";

        public RabbitMqDocumentClientServices(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IModel Connect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();

            }
            catch (Exception)
            {
                _logger.LogInformation("Bağlantı kurulurken bir problem oluştu...");
                throw;
            }


            if (_channel is { IsOpen: true })
            {
                return _channel;
            }

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
            _channel.QueueDeclare(QueueName, true, false, false, null);
            _channel.QueueBind(exchange: ExchangeName, queue: QueueName, routingKey: RoutingDoc);

            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Close();
            _connection?.Dispose();
        }
    }
}
