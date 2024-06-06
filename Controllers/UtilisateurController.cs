using System.Security.Claims;
using Astate.Models;
using Astate.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Astate.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UtilisateurController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUtilisateurService _utilisateurService;
        private readonly ILogger<UtilisateurController> _logger;
        private readonly ITokenService _tokenService;

        public UtilisateurController(UserManager<ApplicationUser> userManager, ITokenService tokenService, ILogger<UtilisateurController> logger, IUtilisateurService utilisateurService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _utilisateurService = utilisateurService;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var profile = new
            {
                Email = user.Email,
                Id = user.Id,
                ProfileImageUrl = user.ProfileImageUrl
            };

            return Ok(profile);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (model.Email == null || model.Password == null)
            {
                return BadRequest("L'email ou le mot de passe ne peut pas être nul");
            }

            if (await _utilisateurService.EmailExistsAsync(model.Email))
            {
                return BadRequest("L'email est déjà utilisé");
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _utilisateurService.CreateUtilisateurAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { UserId = user.Id });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _utilisateurService.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = _tokenService.CreateToken(user);
                return Ok(new { Token = token });
            }
            return BadRequest("Invalid login attempt.");
        }

        [Authorize]
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUser([FromForm] UserDto userDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var result = await _utilisateurService.UpdateUtilisateurAsync(user, userDto);
            if (result.Succeeded)
            {
                return Ok("User updated successfully.");
            }

            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _utilisateurService.DeleteUtilisateurAsync(id);
            if (result.Succeeded)
            {
                return Ok("User deleted successfully.");
            }

            return BadRequest(result.Errors);
        }
    }
}
