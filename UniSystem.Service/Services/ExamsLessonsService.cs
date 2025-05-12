using Microsoft.EntityFrameworkCore;
using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services  // 948c3437-c9a9-4720-88bb-55c37627ce2e
{
    public class ExamsLessonsService : ILessonService
    {
        private readonly AppDbContext _appDbContext;
        public ExamsLessonsService(AppDbContext appDbContext)
        {

            _appDbContext = appDbContext;
        }

        public async Task<List<ExamDto>> StudentLessonExamByDate(LessonListByStudentAndDateRequest model)
        {


            var exams = await _appDbContext.Exams
                .Where(x => x.AcademicianCanGiveLesson.AcademicYear.YearOfEducation == model.AcademicYear && x.AcademicianCanGiveLesson.AcademicYear.Period == model.AcademicPeriodId && x.AppUser.Email == model.Mail)
                .Include(x => x.AcademicianCanGiveLesson.Lesson)
                .Include(x => x.AcademicianCanGiveLesson.AcademicYear)
                .ToListAsync();


            var examDtos = exams.Select(exam => new ExamDto
            {
                AcademicYearId = exam.AcademicianCanGiveLesson.AcademicYearId,

                LessonName = exam.AcademicianCanGiveLesson.Lesson?.Name,
                AcademicYearPeriod = exam.AcademicianCanGiveLesson.AcademicYear.Period,
                AcademicYear = exam.AcademicianCanGiveLesson.AcademicYear.YearOfEducation,
                MidtermExamScore = exam.MidtermExamScore,
                ExamDateDeclareMidterm = exam.ExamDateDeclareMidterm,
                IsChangeableMidterm = exam.IsChangeableMidterm,
                IsTakenMidterm = exam.IsTakenMidterm,
                FinalExamScore = exam.FinalExamScore,
                ExamDateDeclareFinal = exam.ExamDateDeclareFinal,
                IsChangeableFinal = exam.IsChangeableFinal,
                IsTakenFinal = exam.IsTakenFinal,
                ButExamScore = exam.ButExamScore,
                ExamDateDeclareBut = exam.ExamDateDeclareBut,
                IsChangeableBut = exam.IsChangeableBut,
                CanTakeBut = exam.CanTakeBut,
                IsTakenBut = exam.IsTakenBut,

                IsConstant = exam.IsConstant,
                IsPassed = exam.IsPassed,
                Score = exam.Score
            }).ToList();

            return examDtos;
        }


        public async Task<IEnumerable<AcademicYearDto>> AcademicYearsList()
        {
            var val = await _appDbContext.AcademicYears.ToListAsync();
            var resp = val.Select(val => new AcademicYearDto
            {
                Id = val.Id,
                Period = val.Period,
                YearOfEducation = val.YearOfEducation,
            });

            return resp;



        }

    }
}
