using System.Text;
using UniSystem.Core.Models;
using UniSystem.Core.Requests;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;

namespace UniSystem.Service.Services
{
    public class UserRecordServices : IUserRecordServices
    {
        private readonly AppDbContext _appDbContext;

        private readonly IDepartmentService _departmentService;


        public UserRecordServices(AppDbContext appDbContext, IDepartmentService departmentService)
        {
            _appDbContext = appDbContext;
            _departmentService = departmentService;
        }
        public async Task<bool> UserRecord(AppUser user, string department)
        {
            var valdep = await _departmentService.FindDepartmentWithName(department);
            if (valdep != null)
            {
                CreateStudentNumRequest numRequest = new CreateStudentNumRequest()
                {
                    FacNo = valdep.Faculty.Num,
                    StudentEnteredScore = "123",
                    DepNoTotal = valdep.Num


                };

                var num = CreateStudentNum(numRequest);
                user.No = num.ToString();



                StudentDepartment studentDepartment = new StudentDepartment
                {
                    AppUser = user,
                    AppUserId = user.Id,
                    DepartmentId = valdep.Id,
                    Department = valdep,
                };
                var val = _appDbContext.Lessons.Where(x => x.Department.Name == department).ToList();

                for (int i = 0; i < val.Count; i++)
                {

                    StudentLessons studentLessons = new()
                    {
                        AppUser = user,
                        AppUserId = user.Id,
                        Lesson = val[i],
                        LessonId = val[i].Id,

                    };
                    _appDbContext.StudentLessons.Add(studentLessons);
                }
                _appDbContext.StudentDepartments.Add(studentDepartment);
                _appDbContext.SaveChanges();
                return true;


            }
            return false;


        }

        public StringBuilder CreateStudentNum(CreateStudentNumRequest model)
        {

            Random rnd = new Random();
            int count = rnd.Next(100, 1000);
            model.RandomNum = count.ToString();
            StringBuilder StudentNumber = new StringBuilder();
            StudentNumber.Append(model.FacNo);//3
            StudentNumber.Append(model.DepNoTotal); //1
            StudentNumber.Append(model.StudentEnteredScore); //3
            StudentNumber.Append(model.RandomNum); //3


            return StudentNumber;

        }


    }
}
