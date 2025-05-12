using RabbitMQ.Client;
using SharedLayer.RabbitMQ;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly RabbitMqClientService _rabbitMqClientService;
        private IModel _channel;

        public Worker(ILogger<Worker> logger, RabbitMqClientService rabbitMqClientService)
        {

            _rabbitMqClientService = rabbitMqClientService;
            _logger = logger;
        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMqClientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


        }

        //private bool SendMultiMessage(RabbitMqMailMessage model)
        //{
        //    //_context.Messages.Add()
        //}
    }
}
