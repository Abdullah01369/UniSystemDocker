using RabbitMQ.Client;

namespace WorkerService.Services
{
    public class RabbitMqClientWorkerService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory; // rabbitmq bağlantısı için nesne
        private IConnection _connection; // baglantı
        private IModel _channel; //kanal

        public static string ExchangeName = "MailDirectExchange";

        public static string RoutingMail = "mail-route";
        public static string QueueName = "queue-mail";




        public RabbitMqClientWorkerService(ConnectionFactory connectionFactory)
        {

            _connectionFactory = connectionFactory;

        }

        public IModel Connect()
        {
            _connection = _connectionFactory.CreateConnection(); // BAGLANTI OLUŞTUR
            if (_channel is { IsOpen: true })  // BAĞLANTI AÇIK MI KONTROLU YAPIYOR, AÇIKSA CHANNELİ DONDUR
            {
                return _channel;
            }
            _channel = _connection.CreateModel();


            return _channel;

        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _channel = default;
            _connection?.Close();
            _connection?.Dispose();

        }
    }
}
