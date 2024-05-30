using Astate.Models;

namespace Astate.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string userId); 

        Task<Image> GetImageByIdAsync(string userId, string imageId);
    }
}