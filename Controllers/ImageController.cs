using Astate.Services;
using Microsoft.AspNetCore.Mvc;

namespace Astate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _environment;

        public ImageController(IImageService imageService, IWebHostEnvironment environment)
        {
            _imageService = imageService;
            _environment = environment;
        }

        [HttpPost("{userId}/upload")]
        public async Task<IActionResult> Upload(string userId, [FromForm] IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Pas de fichier uploadé.");
            }

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Un ID utilisateur est requis.");
            }

            try
            {
                var fileName = await _imageService.SaveImageAsync(imageFile, userId);
                return Ok(new { fileName });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /**
        **Maybe delete func as not used in front :/
        **/
        [HttpGet("{userId}/{imageId}")]
        public async Task<IActionResult> GetImageAsync(string userId, string imageId)
        {
            var image = await _imageService.GetImageByIdAsync(userId, imageId);
            if (image == null)
            {
                return NotFound();
            }

            var imagePath = Path.Combine(_environment.WebRootPath, "uploads", image.FileName);
            if (!System.IO.File.Exists(imagePath))
            {
                return NotFound();
            }

            var mimeType = GetMimeType(image.FileName);
            var imageFileStream = System.IO.File.OpenRead(imagePath);
            return File(imageFileStream, mimeType);
        }

        private string GetMimeType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream",
            };
        }
    }
}