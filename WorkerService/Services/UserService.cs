using Microsoft.EntityFrameworkCore;
using WorkerService.ModelDto;
using WorkerService.Models;


namespace WorkerService.Services
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;


        public UserService(IServiceScopeFactory scopeFactory, ILogger<UserService> logger)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }


        public async Task<StudentDocumentModel> GetUserInfoForStudentDoc(string StudentNo)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Unisystem3Context>();

                var studentDocument = await context.StudentDepartments
      .Where(x => x.AppUser.No == StudentNo)
      .Include(x => x.AppUser)
      .Include(x => x.Department)
      .Include(x => x.Department.Faculty)
      .Select(x => new StudentDocumentModel
      {
          No = x.AppUser.No,
          Name = x.AppUser.Name,
          Surname = x.AppUser.Surname,
          BornPlace = "ANKARA",
          Photo = x.AppUser.PhotoBase64Text,
          DocDate = DateTime.Now.ToShortDateString(),
          BornDate = x.AppUser.Birthdate.ToShortDateString(),
          FathersName = "baba",
          MothersName = "anne",
          TakingDate = DateTime.Now.ToShortDateString(),
          TC = x.AppUser.Tc,
          Department = x.Department.Name,
          Faculty = x.Department.Faculty.Name
      })
      .FirstOrDefaultAsync();




                return studentDocument;

            }

        }


        public async Task SaveDocInfo(DocumentSavingDto model)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Unisystem3Context>();


                StudentDocument doc = new StudentDocument
                {
                    ExpireAt = DateTime.Now.AddHours(7),
                    CreatedAt = DateTime.Now,
                    DocumentType = model.DocumentType,
                    FilePath = model.FilePath,
                    StudentNo = model.StudentNo,

                };
                await context.StudentDocuments.AddAsync(doc);
                await context.SaveChangesAsync();

            };


        }
    }
}








