using Astate.Models;
using Astate.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Astate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUtilisateurService _utilisateurService;
        private readonly ILogger<UtilisateurController> _logger;
        private readonly ITokenService _tokenService;

        // Injection de UserManager dans le constructeur
        public UtilisateurController(UserManager<IdentityUser> userManager, ITokenService tokenService, ILogger<UtilisateurController> logger, IUtilisateurService utilisateurService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _utilisateurService = utilisateurService;
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

            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _utilisateurService.CreateUtilisateurAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { UserId = user.Id });
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _utilisateurService.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                // Création du token
                var token = _tokenService.CreateToken(user);
                return Ok(new { UserId = user.Id, Token = token });
            }
            return BadRequest("Invalid login attempt.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] IdentityUser updatedUser)
        {
            var user = await _utilisateurService.FindByEmailAsync(updatedUser.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            user.Email = updatedUser.Email;
            user.UserName = updatedUser.Email;

            var result = await _utilisateurService.UpdateUtilisateurAsync(user);
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