using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UniSystem.Service.Services.RabbitMQServices;
using WorkerService.ModelDto;
using WorkerService.Models;


namespace WorkerService
{
    public class GPACalculateWorker : BackgroundService
    {

        private readonly ILogger<GPACalculateWorker> _logger;
        private readonly RabbitMqClientServices _rabbitMqClientServices;
        private IModel _channel;

        private readonly IServiceScopeFactory _scopeFactory;

        private readonly string Routing = "GpaCalculation-route";
        private readonly string Exchange = "GpaCalculationExchange";
        private readonly string QueueName = "queue-gpacalculation";


        public GPACalculateWorker(ILogger<GPACalculateWorker> logger, RabbitMqClientServices rabbitMqClientServices, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _rabbitMqClientServices = rabbitMqClientServices;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMqClientServices.Connect(Exchange, Routing, QueueName);
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
                var departmentData = JsonSerializer.Deserialize<DepartmentMessage>(message);


                // GANO hesaplama işlemi burada yapılacak
                await CalculateGanoForDepartment(departmentData.studentId);

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            return Task.CompletedTask;
        }

        private Task CalculateGanoForDepartment(string studentId)
        {
            _logger.LogInformation($"Öğrenci {studentId} için GANO hesaplanıyor...");

            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<Unisystem3Context>();

                    var totalCredit = context.Exams
            .Where(x => x.AppUserId == studentId && x.IsConstant == true)
            .Include(x => x.AcademicianCanGiveLesson.Lesson)
            .Sum(x => Convert.ToInt32(x.AcademicianCanGiveLesson.Lesson.Credit));

                    if (totalCredit == 0)
                    {
                        goto atla;
                        throw new InvalidOperationException("Toplam kredi sıfır olarak hesaplandı. Öğrencinin ders kredileri eksik veya hatalı.");
                    }


                    var StudentExamListVal = context.Exams
        .Where(exam => exam.AppUserId == studentId)
        .Include(exam => exam.FlagModel)
        .Include(exam => exam.AcademicianCanGiveLesson.Lesson)
        .Select(exam => new GanoCalculateDto
        {
            Flag = exam.FlagModel != null ? exam.FlagModel.Flag : null,
            Credit = int.Parse(exam.AcademicianCanGiveLesson.Lesson.Credit)
        })
        .ToList();

                    if (!StudentExamListVal.Any())
                    {
                        throw new InvalidOperationException("Öğrenciye ait sınav bilgisi bulunamadı.");
                    }

                    double totalval = 0.0;

                    foreach (var item in StudentExamListVal)
                    {
                        double flagscore = FindFlagPoint(item.Flag);
                        totalval = totalval + flagscore * item.Credit;

                    }

                    double Gpa = totalval / totalCredit;

                    var student = context.AspNetUsers.Where(x => x.Id == studentId).FirstOrDefault();
                    student.Gpa = Gpa;
                    context.SaveChanges();

                    _logger.LogInformation($"Öğrenci {studentId} için GANO hesaplama TAMAMLANDI...");

                }
            }
            catch (FormatException ex)
            {
                _logger.LogError($"Format hatası: {ex.Message}");
                throw;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"İşlem hatası: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Bilinmeyen hata: {ex.Message}");
                throw;
            }

        atla:

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.Close();
            _channel?.Dispose();



            return base.StopAsync(cancellationToken);
        }


        public double FindFlagPoint(string flagAbc)
        {
            if (flagAbc == "AA") return 4.0; // AA için 4.0
            if (flagAbc == "BA") return 3.5; // BA için 3.5
            if (flagAbc == "BB") return 3.0; // BB için 3.0
            if (flagAbc == "CB") return 2.5; // CB için 2.5
            if (flagAbc == "CC") return 2.0; // CC için 2.0
            if (flagAbc == "DC") return 1.5; // DC için 1.5
            if (flagAbc == "DD") return 1.0; // DD için 1.0
            if (flagAbc == "FF") return 0.0; // Kaldı (FF) için 0.0

            throw new ArgumentException("Geçersiz not: " + flagAbc);
        }
    }

}
