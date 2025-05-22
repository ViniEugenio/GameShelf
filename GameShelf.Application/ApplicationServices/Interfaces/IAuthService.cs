using System.Security.Claims;

namespace GameShelf.Application.ApplicationServices.Interfaces
{
    public interface IAuthService
    {
        string GerarJWT(ClaimsIdentity claimsIdentity);
    }
}
