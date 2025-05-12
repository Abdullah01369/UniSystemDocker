using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using UniSystem.Service.Services.RabbitMQServices;
using WorkerService;
using WorkerService.Abstracts;
using WorkerService.EmailModels;
using WorkerService.Models;
using WorkerService.Services;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<RabbitMqDocumentPublisherServices>();
        services.AddSingleton<RabbitMqDocumentClientServices>();
        services.AddSingleton<RabbitMqClientServices>();
        services.AddSingleton<UserService>();
        services.AddSingleton<PdfCreatorService>();
        services.AddSingleton<RabbitMqPublisher>();
        services.AddSingleton<IEmailServiceClient, EmailServiceClient>();
        services.AddSingleton<IGradudatedControlService, GradudatedControlService>();
        services.Configure<EmailSettings>(context.Configuration.GetSection("EmailSettings"));  // options pattern
        services.AddHostedService<DocumentGeneratorWorker>();
        services.AddHostedService<GPACalculateWorker>();
        services.AddHostedService<EmailSenderWorker>();
        services.AddHostedService<GradutedControlWorker>();

        services.AddDbContext<Unisystem3Context>(options =>
                   options.UseSqlServer(context.Configuration.GetConnectionString("SqlServer")));


        services.AddSingleton(sp => new ConnectionFactory()
        {
            Uri = new Uri(context.Configuration.GetConnectionString("RabbitMq")),
            DispatchConsumersAsync = true
        });
    })
    .Build();

host.Run();


