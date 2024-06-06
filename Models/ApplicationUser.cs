using Microsoft.AspNetCore.Identity;

namespace Astate.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
    }
}
