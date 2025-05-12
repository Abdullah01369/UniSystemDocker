using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLayer.Dtos;
using UniSystem.Core.Configuration;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Repositories;
using UniSystem.Core.Services;
using UniSystem.Core.UnitOfWork;

namespace UniSystem.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = optionsClient.Value;

            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            var val = await _userManager.GetRolesAsync(user);

            if (user == null) return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);

            // şu rolleme işini düzelt boyle rolleme mi olur amk

            if ((user.IsStudent == true && loginDto.Role == "S") || (user.IsStudent == false && loginDto.Role == "A") || val.Contains("admin"))
            {

                if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
                {
                    return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);
                }
                var token = _tokenService.CreateToken(user);

                var userRefreshToken = await _userRefreshTokenService.Where(x => x.Id == user.Id).SingleOrDefaultAsync();

                if (userRefreshToken == null)
                {
                    await _userRefreshTokenService.AddAsync(new UserRefreshToken { Id = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
                }
                else
                {
                    userRefreshToken.Code = token.RefreshToken;
                    userRefreshToken.Expiration = token.RefreshTokenExpiration;
                }

                await _unitOfWork.CommmitAsync();

                return Response<TokenDto>.Success(token, 200);


            }

            return Response<TokenDto>.Fail("Email or Password is wrong", 400, true);




        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                return Response<ClientTokenDto>.Fail("ClientId or ClientSecret not found", 404, true);
            }

            var token = _tokenService.CreateTokenByClient(client);

            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("Refresh token not found", 404, true);
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.Id);

            if (user == null)
            {
                return Response<TokenDto>.Fail("User Id not found", 404, true);
            }

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommmitAsync();

            return Response<TokenDto>.Success(tokenDto, 200);
        }

        public async Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return Response<NoDataDto>.Fail("Refresh token not found", 404, true);
            }

            _userRefreshTokenService.Remove(existRefreshToken);

            await _unitOfWork.CommmitAsync();

            return Response<NoDataDto>.Success(200);
        }
    }
}
