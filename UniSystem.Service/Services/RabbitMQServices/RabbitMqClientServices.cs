using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace UniSystem.Service.Services.RabbitMQServices
{
    public class RabbitMqClientServices : IDisposable
    {

        private readonly ILogger _logger;
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        // "GpaCalculationExchange";
        // "GpaCalculation-route";
        // "queue-gpacalculation";

        public RabbitMqClientServices(ConnectionFactory connectionFactory, ILogger<RabbitMqClientServices> logger)
        {
            _logger = logger;
            _connectionFactory = connectionFactory;
        }

        public IModel Connect(string exchangename, string routing, string queuename)
        {
            for (int attempt = 1; attempt <= 5; attempt++)
            {
                try
                {
                    if (_connection == null || !_connection.IsOpen)
                    {
                        _connection = _connectionFactory.CreateConnection();
                    }

                    _channel = _connection.CreateModel();

                    // Kanal ayarlarını burada yapıyoruz
                    _channel.ExchangeDeclare(exchangename, type: "direct", true, false);
                    _channel.QueueDeclare(queuename, true, false, false, null);
                    _channel.QueueBind(exchange: exchangename, queue: queuename, routingKey: routing);

                    _logger.LogInformation("RABBITMQ BASARIYLA BAGLANDI");

                    return _channel;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Bağlantı denemesi {attempt}. kez başarısız: {ex.Message}");
                    Thread.Sleep(2000);
                }
            }


            throw new Exception("RabbitMQ bağlantısı 5 denemede kurulamadı.");
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
