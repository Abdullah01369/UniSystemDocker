using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace UniSystem.Service.Services.RabbitMQServices
{
    public class RabbitMqDocumentPublisherServices
    {
        private readonly RabbitMqClientServices _rabbitMQServices;

        public RabbitMqDocumentPublisherServices(RabbitMqClientServices rabbitMQServices)
        {
            _rabbitMQServices = rabbitMQServices;
        }
        public static string ExchangeName = "DocumentExchange";

        public static string RoutingDoc = "document-route";
        public static string QueueName = "queue-document";

        public void PublishStudentDocumentRequest(string studentNo, string type)
        {
            var channel = _rabbitMQServices.Connect(ExchangeName, RoutingDoc, QueueName);

            var bodyString = JsonSerializer.Serialize(new { StudentNo = studentNo, Type = type });
            var bodyBytes = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(
                exchange: RabbitMqDocumentClientServices.ExchangeName,
                routingKey: RabbitMqDocumentClientServices.RoutingDoc,
                basicProperties: properties,
                body: bodyBytes
            );
        }
    }
}
