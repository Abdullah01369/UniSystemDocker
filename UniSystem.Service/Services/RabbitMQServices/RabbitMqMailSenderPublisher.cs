using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using UniSystem.Core.DTOs;

namespace UniSystem.Service.Services.RabbitMQServices
{
    public class RabbitMqMailSenderPublisher
    {
        private readonly RabbitMqClientServices _rabbitMQServices;
        private readonly ILogger<RabbitMqMailSenderPublisher> _logger;
        public RabbitMqMailSenderPublisher(RabbitMqClientServices rabbitMQServices, ILogger<RabbitMqMailSenderPublisher> logger)
        {
            _logger = logger;
            _rabbitMQServices = rabbitMQServices;
        }

        public static string ExchangeName = "EmailSenderExchange";
        public static string RoutingMail = "Email-route";
        public static string QueueName = "queue-Email";

        public void Publish(MailMessageClient mailmessage)
        {
            var channel = _rabbitMQServices.Connect(ExchangeName, RoutingMail, QueueName);
            var bodystring = JsonSerializer.Serialize(mailmessage);
            var bodybyte = Encoding.UTF8.GetBytes(bodystring);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingMail, basicProperties: properties, body: bodybyte);
            _logger.LogInformation("Mail is sended to queue");


        }
    }
}
