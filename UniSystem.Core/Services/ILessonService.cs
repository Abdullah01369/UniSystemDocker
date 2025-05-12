using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;

namespace UniSystem.Core.Services
{
    public interface ILessonService
    {

        public Task<List<ExamDto>> StudentLessonExamByDate(LessonListByStudentAndDateRequest model);
        public Task<IEnumerable<AcademicYearDto>> AcademicYearsList();

    }
}
