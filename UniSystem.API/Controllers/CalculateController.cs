using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Dtos;
using UniSystem.Core.Services;
using UniSystem.Service.Services.RabbitMQServices;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    // [Authorize(Roles = "admin")]
    [ApiController]
    public class CalculateController : CustomBaseController
    {
        private readonly ILogger<CalculateController> _logger;
        private readonly RabbitMqPublisher _rabbitMqPublisher;
        private readonly RabbitMqGradutedService _rabbitMqGradutedService;
        private readonly RabbitMqDocumentPublisherServices _rabbitMqDocumentPublisherServices;
        private readonly IDepartmentService _departmentService;
        private readonly IUserService _userService;

        public CalculateController(ILogger<CalculateController> logger, RabbitMqPublisher rabbitMqPublisher,RabbitMqGradutedService rabbitMqGradutedService, IDepartmentService departmentService, RabbitMqDocumentPublisherServices rabbitMqDocumentPublisherServices, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            _rabbitMqPublisher = rabbitMqPublisher;
            _departmentService = departmentService;
            _rabbitMqDocumentPublisherServices = rabbitMqDocumentPublisherServices;
            _rabbitMqGradutedService = rabbitMqGradutedService;
        }

        [HttpPost("calculate-gano")]
        public async Task<IActionResult> CalculateGano(int departmentId)
        {


            // burada ilgili bolumun ogrencilerini çek kuyruga salla

            var studentlist = await _departmentService.TakeStudentListByDepartment(departmentId);


            foreach (var item in studentlist.ToList())
            {


                _rabbitMqPublisher.PublishStudentGpaCalculation(item.AppUserId);

            }

            return Accepted($"GANO hesaplama isteği bölüme göre kuyruğa eklendi. Bölüm ID: {departmentId}");
        }

        [Authorize]
        [HttpPost("document-request")]
        public async Task<Response<NoDataDto>> DocumentRequest(string StudentNo, string Type)
        {
            if (StudentNo == null || Type == null)
            {
                return Response<NoDataDto>.Fail("Bad Request", 400, true);
            }

            var user = await _userService.FindUserByStudentNo(StudentNo);
            var doclist = await _userService.DocList(user.Email);

            if (doclist.Data != null && doclist.Data.Count == 0)
            {
                _rabbitMqDocumentPublisherServices.PublishStudentDocumentRequest(StudentNo, Type);

                _logger.LogInformation("Belge isteğiniz kuyruğa eklendi.");
                return Response<NoDataDto>.Success(200);
            }
            return Response<NoDataDto>.Success(404);


        }

        [HttpGet]
        public async Task<IActionResult> TakeDoc(int Id)
        {
            if (Id == null) return BadRequest("Id Boş Bırakılamaz");
            var val = await _userService.TakeStudentDoc(Id);

            if (val.Data == null || val.Error != null)
            {
                return ActionResultInstance(val); ;
            }
            var value = val.Data;




            var fileStream = new FileStream(value.FilePath, FileMode.Open, FileAccess.Read);
            return File(fileStream, "application/pdf", Path.GetFileName(value.FilePath));
        }


        [HttpGet("MyDocs")]
        public async Task<IActionResult> DocList(string Email)
        {
            if (Email == null) return BadRequest("Mail Boş Bırakılamaz");
            var user = await _userService.TakeUserByMail(Email);
            var val = await _userService.DocList(user.Email);

            if (val.Data == null || val.Error != null)
            {
                return BadRequest();
            }
            return ActionResultInstance(val); ;




        }


        [HttpPost("calculate-graduted")]
        public async Task<IActionResult> CalculateGraduted(string StudentNo)
        {
            _rabbitMqGradutedService.PublishStudentGradudation(StudentNo);
            return Accepted($"Mezuniyet hesaplama {StudentNo} ' lu öğrenci için başlatılmıştır");
        }

    }
}
