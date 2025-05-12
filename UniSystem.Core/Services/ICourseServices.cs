using SharedLayer.Dtos;
using UniSystem.Core.DTOs;

namespace UniSystem.Core.Services
{
    public interface ICourseServices
    {
        Task<Response<PaggingDto>> AllCourseList(int page, int pageSize, string search);
        Task<Response<PaggingDto>> SearchCourse(string search);
        Task<Response<AddCourseDto>> AddCourse(AddCourseDto model);
        Task<Response<CourseDto>> GetCourseById(int Id);
        Task<Response<EditCourseDto>> EditCourse(EditCourseDto model);
        Task<Response<CourseStatisticDto>> GetStatistic();
    }
}
