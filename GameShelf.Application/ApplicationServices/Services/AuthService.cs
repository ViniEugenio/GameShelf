using GameShelf.Application.ApplicationServices.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class AuthService : IAuthService
    {

        public string GerarJWT(ClaimsIdentity claimsIdentity)
        {

            JwtSecurityTokenHandler handler = new();

            byte[] key = Encoding.ASCII.GetBytes("alskdalskdpaksdpaskdpoaksdopkasopdkapskdpaoksd");
            SigningCredentials credentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(2),
                Subject = claimsIdentity
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);

        }

    }
}
