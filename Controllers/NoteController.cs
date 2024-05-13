using Microsoft.AspNetCore.Mvc;
using Astate.Services;
using Astate.Models;

namespace Astate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        /// <summary>
        /// Récupère une note par son identifiant.
        /// </summary>
        /// <param name="id">L'identifiant de la note à récupérer.</param>
        /// <returns>La note correspondant à l'identifiant.</returns>
        [HttpGet("{id}", Name = "GetNote")]
        public async Task<IActionResult> Get(int id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        /// <summary>
        /// Récupère toutes les notes d'un utilisateur.
        /// </summary>
        /// <param name="userId">L'identifiant de l'utilisateur.</param>
        /// <returns>La liste des notes de l'utilisateur.</returns>
        [HttpGet("{userId}/notes")]
        public async Task<IActionResult> GetAllNotes(int userId)
        {
            var notes = await _noteService.GetNotesByUserIdAsync(userId);
            return Ok(notes);
        }

        /// <summary>
        /// Crée une nouvelle note.
        /// </summary>
        /// <param name="note">La note à créer.</param>
        /// <returns>La réponse HTTP indiquant le succès de la création.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Note note)
        {
            await _noteService.CreateNoteAsync(note);
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        /// <summary>
        /// Met à jour une note existante.
        /// </summary>
        /// <param name="id">L'identifiant de la note à mettre à jour.</param>
        /// <param name="note">La note mise à jour.</param>
        /// <returns>La réponse HTTP indiquant le succès de la mise à jour.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            await _noteService.UpdateNoteAsync(note.Id, note.Content, note.ImageUrl);
            return NoContent();
        }

        /// <summary>
        /// Supprime une note existante.
        /// </summary>
        /// <param name="id">L'identifiant de la note à supprimer.</param>
        /// <returns>La réponse HTTP indiquant le succès de la suppression.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _noteService.DeleteNoteAsync(id);
            return NoContent();
        }
    }
}