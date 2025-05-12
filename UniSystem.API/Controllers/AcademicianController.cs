using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "academician")]
    [ApiController]
    public class AcademicianController : CustomBaseController
    {
        private readonly IAcademicianLessonService _academicianService;
        public AcademicianController(IAcademicianLessonService academicianLessonService)
        {
            _academicianService = academicianLessonService;
        }



        [HttpPost]
        public async Task<IActionResult> ListAcademicianLessons(string UserName)
        {
            var val = await _academicianService.GetAcademiciansActiveLesson(UserName);
            var response = Response<IEnumerable<AcademicianLessonsDto>>.Success(val, 200);
            return ActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> StudentListByCourseForAcademician(StudentListByLessonForAcademicianRequestModel request)
        {
            var val = await _academicianService.StudentInfoByLessonForAcademician(request, "2023-2024", "G");
            var response = Response<IEnumerable<StudentListByLessonForCourseDto>>.Success(val, 200);
            return ActionResultInstance(response);
        }
        [HttpPost]
        public async Task<IActionResult> StudentExamListByCourseForAcademician(StudentListByLessonForAcademicianRequestModel request)
        {
            var val = await _academicianService.ExamsInfoForAcademician(request, "2023-2024", "G");
            var response = Response<IEnumerable<StudentsExamsListForAcademicianByLesson>>.Success(val, 200);
            return ActionResultInstance(response);
        }

        [HttpPost]
        public IActionResult SaveMidtermGrade(SavingMidtermRequestModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _academicianService.SaveMidtermScore(request);
                    return ActionResultInstance(Response<NoDataDto>.Success(200));
                }
                return ActionResultInstance(Response<ErrorDto>.Fail("Başarısız Kayıt İşlemi", 400, true));


            }
            catch (Exception)
            {

                return ActionResultInstance(Response<ErrorDto>.Fail("Başarısız Kayıt İşlemi", 400, true));
            }



        }


        [HttpPost]
        public IActionResult SaveFinal(SavingFinalRequestModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _academicianService.SaveFinalScore(request);
                    return ActionResultInstance(Response<NoDataDto>.Success(200));
                }
                return ActionResultInstance(Response<ErrorDto>.Fail("Başarısız Kayıt İşlemi", 400, true));


            }
            catch (Exception)
            {

                return ActionResultInstance(Response<ErrorDto>.Fail("Başarısız Kayıt İşlemi", 400, true));
            }



        }

        [HttpPost]
        public IActionResult SaveBut(SavingButRequestModel request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _academicianService.SaveButScore(request);
                    return ActionResultInstance(Response<NoDataDto>.Success(200));
                }
                return ActionResultInstance(Response<ErrorDto>.Fail("Başarısız Kayıt İşlemi", 400, true));


            }
            catch (Exception)
            {

                return ActionResultInstance(Response<ErrorDto>.Fail("Başarısız Kayıt İşlemi", 400, true));
            }



        }

    }
}
