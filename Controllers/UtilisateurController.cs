using Astate.Models;
using Microsoft.AspNetCore.Mvc;

namespace Astate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly UtilisateurService _utilisateurService;

        public UtilisateurController(UtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }


        /// <summary>
        /// Récupère un utilisateur par son identifiant unique de manière asynchrone.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à récupérer.</param>
        /// <returns>
        /// Un objet de type Task<IActionResult> représentant le résultat de l'opération.
        /// Si l'utilisateur est trouvé, il renvoie un code de réponse 200 OK avec l'utilisateur dans le corps de la réponse.
        /// Si l'utilisateur n'est pas trouvé, il renvoie un code de réponse 404 Not Found.
        /// Si une erreur se produit lors de la récupération de l'utilisateur, il renvoie un code de réponse 500 Internal Server Error.
        /// </returns>

        [HttpGet("{id}", Name = "GetUtilisateur")]
        public async Task<IActionResult> GetUtilisateurByIdAsync(int id)
        {
            try
            {
                var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);
                return Ok(utilisateur);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                // Gestion des autres exceptions
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }

        /// <summary>
        /// Crée un nouvel utilisateur de manière asynchrone.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur à créer.</param>
        /// <returns>
        /// Un objet de type Task<IActionResult> représentant le résultat de l'opération.
        /// Si l'utilisateur est créé avec succès, il renvoie un code de réponse 201 Created avec l'URI de la ressource nouvellement créée.
        /// Si une erreur se produit lors de la création de l'utilisateur, il renvoie un code de réponse 500 Internal Server Error.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CreateUtilisateur([FromBody] Utilisateur utilisateur)
        {
            try
            {
                await _utilisateurService.CreateUtilisateurAsync(utilisateur);
                return CreatedAtAction(nameof(GetUtilisateurByIdAsync), new { id = utilisateur.Id }, utilisateur);
            }
            catch (Exception ex)
            {
                // Gestion des exceptions
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}"); // Renvoie un code 500 Internal Server Error
            }
        }


    }

}