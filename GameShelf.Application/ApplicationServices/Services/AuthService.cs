using GameShelf.Application.ApplicationServices.Interfaces;
using GameShelf.Application.DTOs.UsuarioDTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameShelf.Application.ApplicationServices.Services
{
    public class AuthService(IOptions<JwtDTO> jwtConfiguration) : IAuthService
    {

        private readonly JwtDTO _jwtConfiguration = jwtConfiguration.Value;

        public string GerarJWT(ClaimsIdentity claimsIdentity)
        {

            JwtSecurityTokenHandler handler = new();

            byte[] key = Encoding.ASCII.GetBytes(_jwtConfiguration.Key);
            SigningCredentials credentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(_jwtConfiguration.ExpireHours),
                Subject = claimsIdentity,
                Issuer = _jwtConfiguration.Issuer,
                Audience = _jwtConfiguration.Audience
            };

            SecurityToken token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);

        }

    }
}
