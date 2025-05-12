using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;

namespace UniSystem.Core.Services
{
    public interface IAcademicianLessonService
    {
        public Task<IEnumerable<AcademicianLessonsDto>> GetAcademiciansActiveLesson(string UserName);
        public Task<List<StudentListByLessonForCourseDto>> StudentInfoByLessonForAcademician(StudentListByLessonForAcademicianRequestModel request, string AcademicYear, string Period);
        public Task<List<StudentsExamsListForAcademicianByLesson>> ExamsInfoForAcademician(StudentListByLessonForAcademicianRequestModel request, string AcademicYear, string Period);
        public void SaveMidtermScore(SavingMidtermRequestModel request);
        public void SaveFinalScore(SavingFinalRequestModel request);
        public void SaveButScore(SavingButRequestModel request);



    }
}
