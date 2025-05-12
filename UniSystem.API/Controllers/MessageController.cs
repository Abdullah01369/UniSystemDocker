using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Dtos;
using SharedLayer.RabbitMQ;
using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;


namespace UniSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class MessageController : CustomBaseController
    {
        IMessagesServices _messagesServices;
        public MessageController(IMessagesServices messagesServices, RabbitMqPublisherService rabbitMqPublisherService)
        {
            _messagesServices = messagesServices;
        }

        [HttpPost]
        public async Task<IActionResult> SendMultiMessage(SendMultiMailModel sendMailRequestDto)
        {
            if (!ModelState.IsValid)
            {

                var response = Response<NoDataDto>.Fail("validation_error", 400, true);
                return ActionResultInstance(response);
            }
            if (sendMailRequestDto == null)
            {
                return BadRequest();

            }

            try
            {
                var val = await _messagesServices.SendMultiMail(sendMailRequestDto, "2023-2024", "G");
                if (val != "true")
                {

                    return ActionResultInstance(Response<NoDataDto>.Fail("Başarısız", 500, true));
                }
                var response = Response<NoDataDto>.Success(200);
                return ActionResultInstance(response);
            }
            catch (Exception)
            {

                return (IActionResult)Response<NoDataDto>.Fail("bir hata ile karşılaşıldı ", 500, true);
            }
        }



        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMailRequestDto sendMailRequestDto)
        {
            if (!ModelState.IsValid)
            {
                var response = Response<NoDataDto>.Fail("validation_error", 400, true);
                return ActionResultInstance(response);
            }
            if (sendMailRequestDto == null)
            {
                return BadRequest();

            }

            try
            {
                var val = await _messagesServices.SendMail(sendMailRequestDto);
                var response = Response<NoDataDto>.Success(200);
                return ActionResultInstance(response);
            }
            catch (Exception)
            {

                return (IActionResult)Response<NoDataDto>.Fail("bir hata ile karşılaşıldı ", 500, true);
            }



        }


        [HttpPost]
        public async Task<IActionResult> InBoxList(string EMail)
        {
            var response = await _messagesServices.InBoxList(EMail);
            return ActionResultInstance(response);

        }
        [HttpPost]
        public async Task<IActionResult> OutBoxList(string EMail)
        {
            var response = await _messagesServices.OutBoxList(EMail);
            return ActionResultInstance(response);

        }
        [HttpPost]
        public async Task<IActionResult> DraftList(string EMail)
        {
            var response = await _messagesServices.DraftList(EMail);
            return ActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> GetInBox(int Id)
        {
            var response = await _messagesServices.GetInBox(Id);
            return ActionResultInstance(response);

        }
        [HttpPost]
        public async Task<IActionResult> DelDraftMail(int Id)
        {
            var response = await _messagesServices.DelDraft(Id);
            return ActionResultInstance(response);

        }
        [HttpPost]
        public async Task<IActionResult> DownloadFile(int Id)
        {
            var response = await _messagesServices.DownloadFile(Id);
            return ActionResultInstance(response);

        }
    }
}
