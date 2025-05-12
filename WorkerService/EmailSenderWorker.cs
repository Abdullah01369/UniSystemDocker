using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UniSystem.Service.Services.RabbitMQServices;
using WorkerService.Abstracts;
using WorkerService.EmailModels;


namespace WorkerService
{
    public class EmailSenderWorker : BackgroundService
    {
        private readonly IEmailServiceClient _emailSenderService;
        private readonly ILogger<EmailSenderWorker> _logger;
        private readonly RabbitMqClientServices _rabbitMQServices; // Doğru türde değişken tanımı
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public static string ExchangeName = "EmailSenderExchange";
        public static string RoutingMail = "Email-route";
        public static string QueueName = "queue-Email";

        public EmailSenderWorker(IServiceProvider provider, ILogger<EmailSenderWorker> logger, RabbitMqClientServices rabbitMqClientService, IEmailServiceClient emailSenderService)
        {
            _emailSenderService = emailSenderService;
            _serviceProvider = provider;
            _rabbitMQServices = rabbitMqClientService;
            _logger = logger;
        }



        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQServices.Connect(ExchangeName, RoutingMail, QueueName);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(QueueName, false, consumer);
            consumer.Received += Consumer_Received;

            return Task.CompletedTask;


        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {

            var response = JsonSerializer.Deserialize<MailMessageClient>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            await _emailSenderService.SendMailAsync(response.MessageContent, response.ReceiverMail);

            _channel.BasicAck(@event.DeliveryTag, false);
            _logger.LogInformation("Mail Gonderildi");
            // calısıyorrrrrrrr

        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
