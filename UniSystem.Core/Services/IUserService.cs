using Microsoft.AspNetCore.Identity;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;

namespace UniSystem.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
        Task<Response<NoDataDto>> CreateUserRoles(string userName);
        Task<Response<UpdateStudentInfoDto>> UpdateStudentInfo(UpdateStudentInfoDto model);
        Task<Response<UserAllInfoDto>> GetUserInfoAll(string userName);
        Task<AppUser> FindUserByStudentNo(string No);
        Task<string> GetDepartmentCodeByUserName(string username);
        Task<Response<DocResponseDto>> TakeStudentDoc(int Id);
        Task<Response<List<DocResponseDto>>> DocList(string studentno);
        Task<AppUser> TakeUserByMail(string email);
        Task<Response<NoDataDto>> ForgetPassword(string email);
        Task<(bool IsValid, string Message)> ValidateResetTokenAsync(string email, string token);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordFinalModel model);
        Task<Response<List<UserAppDto>>> UserList();

    }
}
