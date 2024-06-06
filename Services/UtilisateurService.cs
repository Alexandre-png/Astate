using Astate.Services;
using Astate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Astate.Models
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ImageService _imageService;
        private readonly IWebHostEnvironment _environment;


        public UtilisateurService(UserManager<ApplicationUser> userManager, IWebHostEnvironment environment, ImageService imageService)
        {
            _userManager = userManager;
            _environment = environment;
            _imageService = imageService;
        }


        public async Task<bool> EmailExistsAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> CreateUtilisateurAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUtilisateurAsync(ApplicationUser user, UserDto userDto)
        {
            if (!string.IsNullOrEmpty(userDto.Email))
            {
                user.Email = userDto.Email;
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded && !string.IsNullOrEmpty(userDto.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, userDto.Password);

                if (!passwordResult.Succeeded)
                {
                    return passwordResult;
                }
            }

            if (result.Succeeded && userDto.ProfileImage != null)
            {
                var fileName = await _imageService.SaveProfileImageAsync(userDto.ProfileImage);
                user.ProfileImageUrl = $"/profile_images/{fileName}";
                result = await _userManager.UpdateAsync(user);
            }

            return result;
        }

        private async Task<string> SaveProfileImageAsync(IFormFile imageFile)
        {
            var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<IdentityResult> DeleteUtilisateurAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });
        }
    }
}