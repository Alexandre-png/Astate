
using Microsoft.AspNetCore.Identity;

namespace Astate.Services.Interfaces
{
    public interface IUtilisateurService
    {
        Task<bool> EmailExistsAsync(string email);
        Task<IdentityUser> FindByEmailAsync(string email);
        Task<IdentityResult> CreateUtilisateurAsync(IdentityUser user, string password);
        Task<IdentityResult> UpdateUtilisateurAsync(IdentityUser user);
        Task<IdentityResult> DeleteUtilisateurAsync(string userId);
    }
}