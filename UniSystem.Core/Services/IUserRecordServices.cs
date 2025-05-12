using System.Text;
using UniSystem.Core.Models;
using UniSystem.Core.Requests;

namespace UniSystem.Core.Services
{
    public interface IUserRecordServices
    {
        public Task<bool> UserRecord(AppUser user, string department);


        public StringBuilder CreateStudentNum(CreateStudentNumRequest model);

    }
}
