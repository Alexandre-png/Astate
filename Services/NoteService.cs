using Astate.Models;
using Astate.Data;
using Microsoft.EntityFrameworkCore;

namespace Astate.Services
{
    public class NoteService : INoteService
    {
        private readonly AstateDbContext _context;

        public NoteService(AstateDbContext context)
        {
            _context = context;
        }

        public async Task CreateNoteAsync(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Note>> GetNotesByUserIdAsync(string userId)
        {
            return await _context.Notes.Where(n => n.IdOwner == userId).OrderBy(n => n.DateCreated).ToListAsync();
        }

        public async Task<Note> GetNoteByIdAsync(string id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);
            if (note == null)
            {
                throw new InvalidOperationException("La note demandée n'a pas été trouvée.");
            }
            return note;
        }

        public async Task UpdateNoteAsync(Note noteToUpdate)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteToUpdate.Id);
            if (existingNote == null)
            {
                throw new InvalidOperationException("La note demandée n'a pas été trouvée.");
            }

            existingNote.Title = noteToUpdate.Title ?? existingNote.Title;
            existingNote.Content = noteToUpdate.Content ?? existingNote.Content;
            existingNote.ImageUrl = noteToUpdate.ImageUrl ?? existingNote.ImageUrl;
            _context.Notes.Update(existingNote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(string noteId)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteId);
            if (existingNote == null)
            {
                throw new ArgumentException("La note spécifiée n'existe pas.");
            }

            _context.Notes.Remove(existingNote);
            await _context.SaveChangesAsync();
        }
    }
}