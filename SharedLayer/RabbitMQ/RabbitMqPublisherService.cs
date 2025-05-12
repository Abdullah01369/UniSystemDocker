using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace SharedLayer.RabbitMQ
{
    public class RabbitMqPublisherService
    {
        private readonly RabbitMqClientService _rabbitMQServices;

        public RabbitMqPublisherService(RabbitMqClientService rabbitMQServices)
        {
            _rabbitMQServices = rabbitMQServices;

        }

        public void Publish(RabbitMqMailMessage message)
        {
            var channel = _rabbitMQServices.Connect();
            var bodystring = JsonSerializer.Serialize(message);
            var bodybyte = Encoding.UTF8.GetBytes(bodystring);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMqClientService.ExchangeName, routingKey: RabbitMqClientService.RoutingMail, basicProperties: properties, body: bodybyte);


        }
    }
}
