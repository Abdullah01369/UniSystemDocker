using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UniSystem.Service.Services.RabbitMQServices;
using WorkerService.ModelDto;

using WorkerService.Services;

namespace WorkerService
{
    public class DocumentGeneratorWorker : BackgroundService
    {
        private readonly PdfCreatorService _pdfCreatorService;
        private readonly ILogger<DocumentGeneratorWorker> _logger;
        private readonly RabbitMqClientServices _rabbitMqClientServices;
        private IModel _channel;

        private readonly IServiceScopeFactory _scopeFactory;


        public static string ExchangeName = "DocumentExchange";
        public static string RoutingDoc = "document-route";
        public static string QueueName = "queue-document";

        public DocumentGeneratorWorker(ILogger<DocumentGeneratorWorker> logger, RabbitMqClientServices rabbitMqClientServices, IServiceScopeFactory scopeFactory, PdfCreatorService pdfCreatorService)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _rabbitMqClientServices = rabbitMqClientServices;
            _pdfCreatorService = pdfCreatorService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMqClientServices.Connect(ExchangeName, RoutingDoc, QueueName);
            _channel.BasicQos(0, 1, false);

            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(QueueName, false, consumer);
            consumer.Received += async (sender, eventArgs) =>
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                var Data = JsonSerializer.Deserialize<DocumentWorkerDto>(message);


                await CreateDocument(Data);


                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            return Task.CompletedTask;
        }

        private Task CreateDocument(DocumentWorkerDto req)
        {
            string Type = "";
            if (req.Type == "0")
            {
                Type = "Öğrenci Belgesi";
            }
            if (req.Type == "1")
            {
                Type = "Öğrenci Transkript Belgesi";
            }

            //C:\Users\PC\source\repos\UniSystem\UniSystem.API\wwwroot\files\
            _logger.LogInformation($"Öğrenci {req.StudentNo} için {Type} hazırlanıyor...");

            // string filename = $"{req.StudentNo}_BELGE";
            string filename = $"{req.StudentNo}_BELGE_{Guid.NewGuid()}";

            string outputPath = $@"C:\Users\PC\source\repos\UniSystem\UniSystem.API\wwwroot\files\document\{filename}.pdf";


            _pdfCreatorService.ConvertToPdf(outputPath, req.StudentNo);

            Console.WriteLine("Dönüştürme tamamlandı.");




            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            _channel?.Dispose();
            return base.StopAsync(cancellationToken);
        }




    }
}
