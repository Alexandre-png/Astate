
using Astate.Models;
using Microsoft.AspNetCore.Identity;

namespace Astate.Services.Interfaces
{
    public interface IUtilisateurService
    {
        Task<bool> EmailExistsAsync(string email);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<IdentityResult> CreateUtilisateurAsync(ApplicationUser user, string password);
        Task<IdentityResult> UpdateUtilisateurAsync(ApplicationUser user, UserDto userDto);
        Task<IdentityResult> DeleteUtilisateurAsync(string userId);
    }
}