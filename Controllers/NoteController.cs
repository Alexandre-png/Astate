using Microsoft.AspNetCore.Mvc;
using Astate.Services;
using Astate.Models;
using Microsoft.AspNetCore.Identity;

namespace Astate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly UserManager<IdentityUser> _userManager;

        public NoteController(INoteService noteService, UserManager<IdentityUser> userManager)
        {
            _noteService = noteService;
            _userManager = userManager;
        }

        [HttpGet("{id}", Name = "GetNote")]
        public async Task<IActionResult> Get(string id)
        {
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpGet("{userId}/notes")]
        public async Task<IActionResult> GetAllNotes(string userId)
        {
            var notes = await _noteService.GetNotesByUserIdAsync(userId);
            return Ok(notes);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteDto noteDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByIdAsync(noteDto.IdOwner);

            var note = new Note
            {
                Owner = user,
                Title = noteDto.Title,
                Content = noteDto.Content,
                ImageUrl = noteDto.ImageUrl
            };

            await _noteService.CreateNoteAsync(note);
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Note noteUpdate)
        {
            if (id != noteUpdate.Id)
            {
                return BadRequest("Mauvais ID");
            }

            try
            {
                await _noteService.UpdateNoteAsync(noteUpdate);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _noteService.DeleteNoteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}