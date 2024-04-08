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

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult Get(int id)
        {
            var note = _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpGet("{userId}/notes")]
        public IActionResult GetAllNotes(int userId)
        {
            var notes = _noteService.GetNotesByUserId(userId);
            return Ok(notes);
        }

        // POST: api/Note
        [HttpPost]
        public IActionResult Post([FromBody] Note note)
        {
            _noteService.CreateNote(note);
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        // PUT: api/Note/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Note note)
        {
            if (id != note.Id)
            {
                return BadRequest();
            }

            _noteService.UpdateNote(note.Id, note.Content, note.ImageUrl);
            return NoContent();
        }

        // DELETE: api/Note/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _noteService.DeleteNote(id);
            return NoContent();
        }
    }
}