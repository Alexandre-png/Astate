using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Astate.Models;
using Astate.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Astate.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _key;

        public TokenService(IOptions<Token> options)
        {
            _key = options.Value.SecretKey;
        }
        public string CreateToken(ApplicationUser utilisateur)
        {

            if (utilisateur == null) throw new ArgumentNullException(nameof(utilisateur));
            if (string.IsNullOrEmpty(utilisateur.Id)) throw new ArgumentNullException(nameof(utilisateur.Id));


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, utilisateur.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}