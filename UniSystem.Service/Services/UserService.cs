using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedLayer.Dtos;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Services;
using UniSystem.Data.ConnectionDB;
using UniSystem.Service.Services.RabbitMQServices;

namespace UniSystem.Service.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private RabbitMqMailSenderPublisher _rabbitMqMailSenderPublisher;
        private readonly IUserRecordServices _userRecordServices;

        string baseurl;



        public UserService(AppDbContext appDbContext, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IDepartmentService departmentService, IUserRecordServices userRecordServices, RabbitMqMailSenderPublisher rabbitMqMailSenderPublisher, IConfiguration configuration)
        {
            _rabbitMqMailSenderPublisher = rabbitMqMailSenderPublisher;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _userRecordServices = userRecordServices;

            _roleManager = roleManager;
            _mapper = mapper;
            baseurl = configuration["ForgetPasswordBaseUrl"];
        }


        #region methods
        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new AppUser { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }
            var val = await _userRecordServices.UserRecord(user, createUserDto.DepartmentName);
            if (!val)
            {
                return Response<UserAppDto>.Fail("kayıt esnasında problem oluştu", 404, true);
            }
            return Response<UserAppDto>.Success(_mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<NoDataDto>> ForgetPassword(string email)
        {
            var hasuser = await _userManager.FindByEmailAsync(email);

            if (hasuser == null)
            {
                return Response<NoDataDto>.Fail("User not found", 404, true);
            }

            string passwordResetToken = await _userManager.GenerateUserTokenAsync(hasuser, TokenOptions.DefaultProvider.ToString(), "ResetPassword");

            string resetPasswordUrl = $"{baseurl}/Home/ResetPassword?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(passwordResetToken)}";

            MailMessageClient model = new MailMessageClient()
            {
                ReceiverMail = email,
                MessageContent = $"<p>Şifrenizi sıfırlamak için <a href=\"{resetPasswordUrl}\">buraya tıklayın</a>.</p>"


            };
            _rabbitMqMailSenderPublisher.Publish(model);

            return Response<NoDataDto>.Success(StatusCodes.Status200OK);
        }
        public async Task<(bool IsValid, string Message)> ValidateResetTokenAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return (false, "Kullanıcı bulunamadı.");
            }
            var provider = TokenOptions.DefaultProvider;
            var isValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider.ToString(), "ResetPassword", token.ToString());
            if (isValid)
            {
                return (true, "Token geçerli.");
            }
            return (false, "Geçersiz token.");
        }
        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordFinalModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Kullanıcı bulunamadı." });
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            return result;
        }
        public async Task<Response<NoDataDto>> CreateUserRoles(string userName)
        {

            if (!await _roleManager.RoleExistsAsync("student"))
            {
                await _roleManager.CreateAsync(new() { Name = "student" });

            }

            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, "student");



            return Response<NoDataDto>.Success(StatusCodes.Status201Created);

        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserAppDto>.Fail("UserName not found", 404, true);
            }

            return Response<UserAppDto>.Success(_mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<UserAllInfoDto>> GetUserInfoAll(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserAllInfoDto>.Fail("UserName not found", 404, true);
            }
            var address = _appDbContext.Addresses.Where(x => x.Id == user.AddressId).FirstOrDefault();
            var responser = Response<UserAllInfoDto>.Success(_mapper.Map<UserAllInfoDto>(user), 200);
            //    responser.Data.AddressDec = address.Declaration;
            return responser;


        }

        public async Task<Response<UpdateStudentInfoDto>> UpdateStudentInfo(UpdateStudentInfoDto model)
        {
            try
            {
                // Kullanıcıyı bul
                var user = await _userManager.FindByNameAsync(model.username);

                if (user == null)
                {
                    return Response<UpdateStudentInfoDto>.Fail("UserName not found", 404, true);
                }
                user.PhoneNumber = model.phone;

                var addressId = user.AddressId;

                if (addressId == null)
                {
                    // Yeni adres oluştur
                    Address newAddress = new Address()
                    {
                        Declaration = model.city
                    };

                    // Yeni adresi veritabanına ekle
                    _appDbContext.Addresses.Add(newAddress);
                    await _appDbContext.SaveChangesAsync();

                    // Kullanıcıya yeni adresi ata
                    user.Address = newAddress;
                    user.AddressId = newAddress.Id;
                }
                else
                {
                    // Mevcut adresi güncelle
                    var address = await _appDbContext.Addresses.FindAsync(addressId);

                    if (address != null)
                    {
                        address.Declaration = model.city;
                        _appDbContext.Addresses.Update(address);
                    }
                }

                // Kullanıcıyı güncelle
                _appDbContext.Users.Update(user);
                await _appDbContext.SaveChangesAsync();

                return Response<UpdateStudentInfoDto>.Success(200);
            }
            catch (Exception)
            {
                return Response<UpdateStudentInfoDto>.Fail("Güncelleme esnasında bir sorunla karşılaşıldı", 500, true);
            }
        }

        public async Task<AppUser> FindUserByStudentNo(string No)
        {
            var val = await _appDbContext.Users.Where(x => x.No == No && x.IsStudent == true).FirstOrDefaultAsync();
            return val;

        }

        public async Task<string> GetDepartmentCodeByUserName(string username)
        {
            string code = "";
            var val = await _appDbContext.StudentDepartments.Where(x => x.AppUser.UserName == username).Include(x => x.AppUser).Include(x => x.Department).FirstOrDefaultAsync();
            if (val != null)
            {
                code = val.Department.Code;

            }
            return code;

        }

        public async Task<Response<DocResponseDto>> TakeStudentDoc(int Id)
        {
            var document = await _appDbContext.StudentDocuments.Where(d => d.Id == Id && d.ExpireAt > DateTime.Now).FirstOrDefaultAsync();


            if (document == null)
            {

                return Response<DocResponseDto>.Fail("Belge bulunamadı veya süresi dolmuş", 404, true);
            }

            DocResponseDto docResponseDto = new DocResponseDto()
            {

                FilePath = document.FilePath,
                Id = document.Id,



            };

            return Response<DocResponseDto>.Success(docResponseDto, 200);
        }

        public async Task<Response<List<DocResponseDto>>> DocList(string Email)
        {
            var val = await _userManager.FindByEmailAsync(Email);
            if (val == null)
            {
                return Response<List<DocResponseDto>>.Fail("Hatalı numara", 404, true);
            }
            var document = await _appDbContext.StudentDocuments.Where(d => d.StudentNo == val.No && d.ExpireAt > DateTime.Now).ToListAsync();
            List<DocResponseDto> list = new List<DocResponseDto>();

            if (document == null)
            {

                return Response<List<DocResponseDto>>.Fail("Belge bulunamadı veya süresi dolmuş", 404, true);
            }


            foreach (var item in document)
            {
                DocResponseDto doc = new DocResponseDto()
                {
                    StudentNo = item.StudentNo,

                    Id = item.Id,
                    Message = "Öğrenci Belgesi"

                };
                list.Add(doc);
            }


            return Response<List<DocResponseDto>>.Success(list, 200);
        }

        public async Task<AppUser> TakeUserByMail(string email)
        {
            var val = await _userManager.FindByEmailAsync(email);
            return val;
        }

        #endregion

        public async Task<Response<List<UserAppDto>>> UserList()
        {


            var result = await _userManager.Users.ToListAsync();
            var val = _mapper.Map<List<UserAppDto>>(result);


            return Response<List<UserAppDto>>.Success(val, 200);
        }



    }
}