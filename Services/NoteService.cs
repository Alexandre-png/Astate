using Astate.Models;
using Astate.Data;
using Microsoft.EntityFrameworkCore;

namespace Astate.Services
{
    public class NoteService
    {
        private readonly AstateDbContext _context;

        public NoteService(AstateDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Crée une nouvelle note de manière asynchrone.
        /// </summary>
        /// <param name="note">La note à créer.</param>
        /// <returns>Une tâche asynchrone représentant l'opération.</returns>
        public async Task CreateNoteAsync(Note note)
        {
            // Génération de l'ID de la note
            note.Id = 0;

            // Ajout de la note à la base de données
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Récupère toutes les notes d'un utilisateur donné de manière asynchrone.
        /// </summary>
        /// <param name="userId">L'identifiant de l'utilisateur.</param>
        /// <returns>Une liste des notes de l'utilisateur.</returns>
        public async Task<List<Note>> GetNotesByUserIdAsync(int userId)
        {
            return await _context.Notes.Where(n => n.IdOwner == userId).ToListAsync();
        }

        /// <summary>
        /// Récupère une note par son identifiant de manière asynchrone.
        /// </summary>
        /// <param name="id">L'identifiant de la note à récupérer.</param>
        /// <returns>La note correspondant à l'identifiant.</returns>
        public async Task<Note> GetNoteByIdAsync(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                throw new InvalidOperationException("La note demandée n'a pas été trouvée.");
            }

            return note;
        }

        /// <summary>
        /// Met à jour une note existante de manière asynchrone.
        /// </summary>
        /// <param name="noteId">L'identifiant de la note à mettre à jour.</param>
        /// <param name="newContent">Le nouveau contenu de la note.</param>
        /// <param name="newImageUrl">La nouvelle URL de l'image de la note.</param>
        /// <returns>Une tâche asynchrone représentant l'opération.</returns>
        public async Task UpdateNoteAsync(int noteId, string newContent = null, string newImageUrl = null)
        {
            var existingNote = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteId);

            if (existingNote == null)
            {
                throw new ArgumentException("La note spécifiée n'existe pas.");
            }

            if (newContent != null)
            {
                existingNote.Content = newContent;
            }

            if (newImageUrl != null)
            {
                existingNote.ImageUrl = newImageUrl;
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime une note existante de manière asynchrone.
        /// </summary>
        /// <param name="noteId">L'identifiant de la note à supprimer.</param>
        /// <returns>Une tâche asynchrone représentant l'opération.</returns>
        public async Task DeleteNoteAsync(int noteId)
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