using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class LessonController : CustomBaseController
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {

            _lessonService = lessonService;
        }

        [HttpPost]
        public async Task<IActionResult> LessonListByStudentAndPeriod(LessonListByStudentAndDateRequest request)
        {
            var val = await _lessonService.StudentLessonExamByDate(request);

            var response = Response<List<ExamDto>>.Success(val, 200);

            return ActionResultInstance(response);
        }

        [HttpGet]
        public IActionResult DownloadLessonProgramByDepartment(string code)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/lessonprograms", $"{code}.pdf");

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("PDF dosyası bulunamadı");
            }


            var pdfBytes = System.IO.File.ReadAllBytes(filePath);

            return File(pdfBytes, "application/pdf", $"{code}.pdf");
        }


        [HttpGet]
        public async Task<IActionResult> ListAcademicYear()
        {
            var val = await _lessonService.AcademicYearsList();
            var response = Response<IEnumerable<AcademicYearDto>>.Success(val, 200);
            return ActionResultInstance(response);
        }

    }
}
