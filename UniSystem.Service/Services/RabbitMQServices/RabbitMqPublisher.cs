using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace UniSystem.Service.Services.RabbitMQServices
{
    public class RabbitMqPublisher
    {

        private readonly RabbitMqClientServices _rabbitMQServices;

        private readonly string Routing = "GpaCalculation-route";
        private readonly string Exchange = "GpaCalculationExchange";
        private readonly string QueueName = "queue-gpacalculation";


        public RabbitMqPublisher(RabbitMqClientServices rabbitMQServices)
        {
            _rabbitMQServices = rabbitMQServices;
        }

        public void PublishStudentGpaCalculation(string studentid)
        {
            var channel = _rabbitMQServices.Connect(Routing, Exchange, QueueName);

            var bodyString = JsonSerializer.Serialize(new { studentId = studentid });
            var bodyBytes = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(
                exchange: Exchange,
                routingKey: Routing,
                basicProperties: properties,
                body: bodyBytes
            );
        }
    }
}
