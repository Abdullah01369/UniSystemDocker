using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkerService.Abstracts;
using WorkerService.ModelDto;
using WorkerService.Models;

namespace WorkerService.Services
{
    public class GradudatedControlService:IGradudatedControlService
    {

        private readonly ILogger<UserService> _logger;

        private readonly IServiceScopeFactory _scopeFactory;


        public GradudatedControlService(IServiceScopeFactory scopeFactory, ILogger<UserService> logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }
        // deneme

        public async Task<GradutedDto> StudentGradutedControl(string StudentNo)
        {
            GradutedDto gradutedDto = new GradutedDto();
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<Unisystem3Context>();

                    var student = await context.AspNetUsers.Where(x => x.No == StudentNo).FirstOrDefaultAsync();

                    var gpacontrol = await context.AspNetUsers.Where(x => x.No == StudentNo).Select(x => x.Gpa).FirstOrDefaultAsync();
                    var IsPassedAllExam = await context.Exams.Where(x => x.AppUserId == student.Id).Select(x => x.IsPassed != true).CountAsync();

                    //var creditcount = await context.Exams
                    //    .Where(x => x.AppUserId == student.Id)
                    //     .Select(x => int.Parse(x.AcademicianCanGiveLesson.Lesson.Credit))
                    //         .SumAsync();

                    // olumsuz ise true doner
                    var hasAnyMissingScores = await context.Exams.AnyAsync(x =>
                        x.MidtermExamScore == null ||
                        x.FinalExamScore == null ||
                        (x.IsTakenBut == true && x.ButExamScore == null)
                    );

                    if (gpacontrol >= 2.0) gradutedDto.IsGpaGreatherThanTwo = true; else gradutedDto.IsGpaGreatherThanTwo = false;
                    if (IsPassedAllExam == 0) gradutedDto.IsPassedAllCourse = true; else gradutedDto.IsPassedAllCourse = false;
                   // if (creditcount >= 240) gradutedDto.CreditGreather240 = true; else gradutedDto.CreditGreather240 = false;
                    if (hasAnyMissingScores != true) gradutedDto.AllExamsScoreEntered = true; gradutedDto.AllExamsScoreEntered = false;
                    gradutedDto.GPA = gpacontrol.ToString();

                    GradutedStatusStudent entity = new GradutedStatusStudent()
                    {
                        AppUserId = student.Id,
                        CreditGreather240 = gradutedDto.CreditGreather240,
                        GPA = gradutedDto.GPA,
                        IsGpaGreatherThanTwo = gradutedDto.IsGpaGreatherThanTwo,
                        ISOkeyIntern = gradutedDto.ISOkeyIntern,
                        IsPassedAllCourse = gradutedDto.IsPassedAllCourse,
                        AllExamsScoreEntered = gradutedDto.AllExamsScoreEntered,
                        CreditDesc = gradutedDto.CreditDesc,


                    };

                    context.GradutedStatusStudents.Add(entity);
                    context.SaveChanges();


                    return gradutedDto;

                }

            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}
