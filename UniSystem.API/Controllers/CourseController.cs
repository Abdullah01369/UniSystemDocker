using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Services;

namespace UniSystem.API.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    [Authorize(Roles = "admin")]
    public class CourseController : CustomBaseController
    {
        private readonly ICourseServices _course;
        private readonly IDepartmentService _department;
        public CourseController(ICourseServices courseServices, IDepartmentService departmentService)
        {
            _department = departmentService;
            _course = courseServices;

        }
        [HttpGet("GetCourse")]
        public async Task<IActionResult> GetCourse(int Id)
        {
            var val = await _course.GetCourseById(Id);
            return ActionResultInstance(val);
        }
        [HttpPost("EditCourse")]
        public async Task<IActionResult> EditCourse(EditCourseDto courseDto)
        {
            var val = await _course.EditCourse(courseDto);
            return ActionResultInstance(val);
        }
        [HttpGet("GetAllCourse")]
        public async Task<IActionResult> GetAllCourseAsync(int page = 1, int pageSize = 10, string search = "")
        {
            var val = await _course.AllCourseList(page, pageSize, search);
            return ActionResultInstance(val);
        }



        [HttpGet("SearchCourse")]
        public async Task<IActionResult> SearchCourse(string input)
        {
            var val = await _course.SearchCourse(input);
            return ActionResultInstance(val);
        }

        [HttpGet("GetCourseStatistic")]
        public async Task<IActionResult> GetCourseStatistic()
        {
            var val = await _course.GetStatistic();
            return ActionResultInstance(val);
        }

        [HttpPost("AddCourse")]
        public async Task<IActionResult> AddCourse(AddCourseDto model)
        {


            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
          .SelectMany(v => v.Errors)
          .Select(e => e.ErrorMessage)
          .ToList();

                var errorresponse = Response<NoDataDto>.Fail(new ErrorDto(errors, true), 400);
                return ActionResultInstance(errorresponse);

            }
            var val = await _course.AddCourse(model);
            return ActionResultInstance(val);
        }

        [HttpGet("DepartmentList")]
        public async Task<IActionResult> DepartmentList()
        {
            var val = await _department.GetAllDepartment();
            return ActionResultInstance(val);
        }
    }
}
