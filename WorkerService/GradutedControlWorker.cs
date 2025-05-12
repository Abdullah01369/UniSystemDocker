using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Abstracts;
using UniSystem.Service.Services.RabbitMQServices;

using WorkerService.EmailModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace WorkerService
{
    public class GradutedControlWorker: BackgroundService
    {

        private readonly IGradudatedControlService _gradudatedControlService;
        private readonly ILogger<GradutedControlWorker> _logger;
        private readonly RabbitMqClientServices _rabbitMQServices; 
        private readonly IServiceProvider _serviceProvider;
        private IModel _channel;

        public static string ExchangeName = "GradutedControlExchange";
        public static string RoutingMail = "GradutedControl-route";
        public static string QueueName = "queue-GradutedControl";

        public GradutedControlWorker(IServiceProvider provider, ILogger<GradutedControlWorker> logger, RabbitMqClientServices rabbitMqClientService, IGradudatedControlService emailSenderService)
        {
            _gradudatedControlService = emailSenderService;
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

            var response = JsonSerializer.Deserialize<string>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            _logger.LogInformation($"Mezuniyet durumu {response} numaralı öğrenci için hesaplama başlandı.");

             await _gradudatedControlService.StudentGradutedControl(response);

            _channel.BasicAck(@event.DeliveryTag, false);
            _logger.LogInformation($"Mezuniyet durumu {response} numaralı öğrenci için hesaplandı.");
         

        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}

