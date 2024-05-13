using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Astate.Models;
using Astate.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;

namespace Astate.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _key;

        public TokenService(IOptions<Token> options)
    {
        _key = options.Value.SecretKey;
    }
        public string CreateToken(IdentityUser utilisateur)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString()),
                    new Claim(ClaimTypes.Email, utilisateur.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}