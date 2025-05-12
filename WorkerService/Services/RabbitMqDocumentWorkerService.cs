using RabbitMQ.Client;

namespace WorkerService.Services
{
    public class RabbitMqDocumentWorkerService : IDisposable
    {
        private readonly ConnectionFactory _connectionFactory; // rabbitmq bağlantısı için nesne
        private IConnection _connection; // baglantı
        private IModel _channel; //kanal

        public static string ExchangeName = "DocumentExchange";

        public static string RoutingDoc = "document-route";
        public static string QueueName = "queue-document";




        public RabbitMqDocumentWorkerService(ConnectionFactory connectionFactory)
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
