using Microsoft.EntityFrameworkCore;
using UniSystem.Core.DTOs;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services
{
    public class AcademicianLessonService : IAcademicianLessonService
    {
        private readonly AppDbContext _context;
        public AcademicianLessonService(AppDbContext _appDbContext)
        {
            _context = _appDbContext;

        }
        public async Task<IEnumerable<AcademicianLessonsDto>> GetAcademiciansActiveLesson(string UserName)
        {
            var val = await _context.AcademicianCanGiveLessons.Where(x => x.AppUser.UserName == UserName && x.IsActive == true).Include(x => x.AcademicYear).Include(x => x.Lesson).Include(x => x.AppUser).ToListAsync();

            var resp = val.Select(val => new AcademicianLessonsDto
            {
                LessonName = val.Lesson.Name,
                LessonId = val.LessonId,
                AcademicYearPeriod = val.AcademicYear.Period,
                IsActive = val.IsActive,
                AcademicYear = val.AcademicYear.YearOfEducation,
                AcademicYearId = val.AcademicYearId,
                AppName = val.AppUser.UserName,
                AppUserId = val.AppUserId,
                LessonCode = val.Lesson.Code

            });

            return resp;


        }



        public async Task<List<StudentListByLessonForCourseDto>> StudentInfoByLessonForAcademician(StudentListByLessonForAcademicianRequestModel request, string AcademicYear, string Period)
        {
            if (request != null)
            {
                var val = await _context.Exams.Where(x => x.AcademicianCanGiveLesson.AppUser.Email == request.AcademicianMail && x.AcademicianCanGiveLesson.LessonId == request.LessonId && x.AcademicianCanGiveLesson.AcademicYear.YearOfEducation == AcademicYear && x.AcademicianCanGiveLesson.AcademicYear.Period == Period).Include(x => x.AppUser).ToListAsync();

                var resp = val.Select(val => new StudentListByLessonForCourseDto
                {
                    Id = val.AppUserId,
                    Mail = val.AppUser.Email,
                    Name = val.AppUser.Name,
                    Surname = val.AppUser.Surname,
                    StudentNo = val.AppUser.No,
                    PhonenNumber = val.AppUser.PhoneNumber,
                    Photo = val.AppUser.PhotoBase64Text


                });
                return resp.ToList();
            }
            return null;
        }


        public async Task<List<StudentsExamsListForAcademicianByLesson>> ExamsInfoForAcademician(StudentListByLessonForAcademicianRequestModel request, string AcademicYear, string Period)
        {
            if (request != null)
            {
                var val = await _context.Exams.Where(x => x.AcademicianCanGiveLesson.AppUser.Email == request.AcademicianMail && x.AcademicianCanGiveLesson.LessonId == request.LessonId && x.AcademicianCanGiveLesson.AcademicYear.YearOfEducation == AcademicYear && x.AcademicianCanGiveLesson.AcademicYear.Period == Period).Include(x => x.AppUser).Include(x => x.FlagAbc).Include(x => x.AcademicianCanGiveLesson.Lesson).ToListAsync();

                var resp = val.Select(val => new StudentsExamsListForAcademicianByLesson
                {
                    Id = val.Id,
                    Name = val.AppUser.Name,
                    Surname = val.AppUser.Surname,
                    Flag = val.FlagAbc?.Flag ?? "--",
                    ButExamScore = val.ButExamScore,
                    CanTakeBut = val.CanTakeBut,
                    ExamDateDeclareBut = val.ExamDateDeclareBut,
                    ExamDateDeclareFinal = val.ExamDateDeclareFinal,
                    ExamDateDeclareMidterm = val.ExamDateDeclareMidterm,
                    FinalExamScore = val.FinalExamScore,
                    IsChangeableBut = val.IsChangeableBut,
                    IsChangeableFinal = val.IsChangeableFinal,
                    IsChangeableMidterm = val.IsChangeableMidterm,
                    IsConstant = val.IsConstant,
                    IsPassed = val.IsPassed,
                    IsTakenBut = val.IsTakenBut,
                    IsTakenFinal = val.IsTakenFinal,
                    IsTakenMidterm = val.IsTakenMidterm,
                    LessonName = val.AcademicianCanGiveLesson.Lesson.Name,
                    Level = "deneme sınıf", // SINIF EKLENECEK
                    MidtermExamScore = val.MidtermExamScore,
                    Score = val.Score,
                    StudentNo = val.AppUser.No


                });
                return resp.ToList();
            }
            return null;
        }

        public void SaveMidtermScore(SavingMidtermRequestModel request)
        {
            // kontrolleri yap  
            var val = _context.Database.ExecuteSqlInterpolated($"EXEC InsertStudentMidtermExamScore {request.StudentNo}, {request.ExamId},{request.MidtermScore}");



        }

        public void SaveFinalScore(SavingFinalRequestModel request)
        {

            var val = _context.Database.ExecuteSqlInterpolated($"EXEC InsertFinalExam {request.StudentNo}, {request.ExamId},{request.FinalScore}");


        }
        public void SaveButScore(SavingButRequestModel request)
        {

            var val = _context.Database.ExecuteSqlInterpolated($"EXEC InsertButScore {request.StudentNo}, {request.ExamId},{request.ButScore}");


        }
    }
}
