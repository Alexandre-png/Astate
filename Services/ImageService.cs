using Astate.Data;
using Astate.Models;
using Microsoft.EntityFrameworkCore;

namespace Astate.Services
{
    public class ImageService : IImageService
    {
        private readonly AstateDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ImageService(AstateDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<Image> GetImageByIdAsync(string userId, string imageId)
        {
            var UID = _context.Users.FirstOrDefault(u => u.Id == userId);
            return await _context.Images.FirstOrDefaultAsync(i => i.Id == imageId && i.UploadedBy == UID);
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string userId)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Invalid image file");

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

            var image = new Image
            {
                Id = Guid.NewGuid().ToString(),
                FileName = fileName,
                FilePath = filePath,
                ContentType = imageFile.ContentType,
                UploadedAt = DateTime.UtcNow,
                UploadedBy = _context.Users.FirstOrDefault(u => u.Id == userId)
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return fileName;
        }

        public async Task<string> SaveProfileImageAsync(IFormFile imageFile)
        {
            var uploadsPath = Path.Combine(_environment.WebRootPath, "profile_images");
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
    }
}