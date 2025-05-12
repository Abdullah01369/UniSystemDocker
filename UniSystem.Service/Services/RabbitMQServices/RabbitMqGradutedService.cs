using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace UniSystem.Service.Services.RabbitMQServices
{
    public class RabbitMqGradutedService
    {
        private readonly RabbitMqClientServices _rabbitMQServices;

        private static readonly string Routing = "GradutedControl-route";
        private static readonly string Exchange = "GradutedControlExchange";
        private static readonly string QueueName = "queue-GradutedControl";
       


        public RabbitMqGradutedService(RabbitMqClientServices rabbitMQServices)
        {
            _rabbitMQServices = rabbitMQServices;
        }

        public void PublishStudentGradudation(string studentno)
        {
            var channel = _rabbitMQServices.Connect(Routing, Exchange, QueueName);

            var bodyString = JsonSerializer.Serialize(studentno);
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
