using Astate.Models;
using Microsoft.AspNetCore.Identity;

namespace Astate.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser utilisateur);
    }
}