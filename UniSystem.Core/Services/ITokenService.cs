
using UniSystem.Core.Configuration;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;

namespace UniSystem.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser userApp);

        ClientTokenDto CreateTokenByClient(Client client);
    }
}
