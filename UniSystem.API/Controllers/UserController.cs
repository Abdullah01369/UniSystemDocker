using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Services;

namespace UniSystem.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IGraduateService _graduateService;


        public UserController(IUserService userService, UserManager<AppUser> userManager, IGraduateService graduateService)
        {
            _userManager = userManager;
            _userService = userService;
            _graduateService = graduateService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            return ActionResultInstance(await _userService.CreateUserAsync(createUserDto));
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
        [HttpPost("GetUserByName/{username}")]
        public async Task<IActionResult> GetUserByName(string username)
        {
            return ActionResultInstance(await _userService.GetUserInfoAll(username));
        }
        [HttpPost("CreateUserRoles/{userName}")]
        public async Task<IActionResult> CreateUserRoles(string userName)
        {

            return ActionResultInstance(await _userService.CreateUserRoles(userName));

        }
        [HttpPost("UpdateStudentInfo")]
        public async Task<IActionResult> UpdateStudentInfo(UpdateStudentInfoDto model)
        {
            return ActionResultInstance(await _userService.UpdateStudentInfo(model));
        }
        [HttpPost("FindUserByStudentNo")]
        public async Task<IActionResult> FindUserByStudentNo(string StudentNo)
        {
            var val = await _userService.FindUserByStudentNo(StudentNo);

            if (val == null)
            {
                var resperr = Response<StudentDto>.Fail("Öğrenci Bulunamadı", 200, true);
                return ActionResultInstance(resperr);

            }
            StudentDto student = new StudentDto()
            {
                Id = val.Id,
                Name = val.Name,
                No = val.No,
                Surname = val.Surname,
            };

            var resp = Response<StudentDto>.Success(student, 200);
            return ActionResultInstance(resp);

        }
        [HttpPost("TakeDepartmentCode")]
        public async Task<IActionResult> TakeDepartmentCode(string username)
        {

            var val = _userService.GetDepartmentCodeByUserName(username);

            DepartmentCodeDto model = new DepartmentCodeDto
            {
                Code = val.Result
            };


            var resp = Response<DepartmentCodeDto>.Success(model, 200);
            return ActionResultInstance(resp);

        }
        [AllowAnonymous]
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var val = await _userService.ForgetPassword(email);
            return ActionResultInstance(val);
        }
        [AllowAnonymous]
        [HttpPost("ResetPasswordTokenValidate")]
        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            var result = await _userService.ValidateResetTokenAsync(email, token);

            if (!result.IsValid)
            {
                var response = Response<NoDataDto>.Fail("süresi geçmiş bir işlem gerçekleştiriyorsunuz, tekrar mail alın", 404, true);
                return ActionResultInstance(response);
            }
            ResetPasswordModel model = new ResetPasswordModel
            {
                Email = email,
                IsSuccess = true,
                Token = token
            };
            var responsesuccess = Response<ResetPasswordModel>.Success(model, 200);
            return ActionResultInstance<ResetPasswordModel>(responsesuccess);
        }
        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPasswordFinal(ResetPasswordFinalModel model)
        {
            if (!ModelState.IsValid)
            {
                var successresponse = Response<NoDataDto>.Fail("Boş veri kabul edilemez", 400, true);
                return ActionResultInstance(successresponse);
            }

            var val = await _userService.ResetPasswordAsync(model);
            if (val.Succeeded)
            {
                var successresponse = Response<NoDataDto>.Success(201);
                return ActionResultInstance(successresponse);
            }

            var errors = val.Errors.Select(x => x.Description).ToList();
            var errorresponse = Response<NoDataDto>.Fail(new ErrorDto(errors, true), 400);

            return ActionResultInstance(errorresponse);

        }

        [HttpGet("GetUserList")]
        public async Task<IActionResult> UserList()
        {
            var val = await _userService.UserList();

            return ActionResultInstance(val);
        }


         [HttpGet("GetGraduatedInfo")]
        public async Task<IActionResult> GetGraduatedInfo(string Email)
        {
            var val = await _graduateService.GetGradutedInfo(Email);

            return ActionResultInstance(val);
        }
    }
}
