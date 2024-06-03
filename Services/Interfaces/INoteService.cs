using Astate.Models;

namespace Astate.Services
{
    public interface INoteService
    {
        Task CreateNoteAsync(Note note);
        Task<List<Note>> GetNotesByUserIdAsync(string userId);
        Task<Note> GetNoteByIdAsync(string id);
        Task UpdateNoteAsync(Note noteToUpdate);
        Task DeleteNoteAsync(string noteId);
    }
}