using Astate.Models;
using Astate.Data;

namespace Astate.Services
{
    public class NoteService
    {
        private readonly AstateDbContext _context; 

        public NoteService(AstateDbContext context)
        {
            _context = context;
        }

        // Méthode pour créer une nouvelle note
        public void CreateNote(Note note)
        {
            // Génération de l'ID de la note
            note.Id = 0;

            // Ajout de la note à la base de données
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        // Méthode pour récupérer toutes les notes d'un utilisateur donné
        public List<Note> GetNotesByUserId(int userId)
        {
            return _context.Notes.Where(n => n.IdOwner == userId).ToList();
        }

        public Note GetNoteById(int id)
        {
            var note = _context.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
            {
                throw new InvalidOperationException("La note demandée n'a pas été trouvée.");
            }

            return note;
        }


        public void UpdateNote(int noteId, string newContent = null, string newImageUrl = null)
        {
            // Récupérer la note existante à partir de la base de données
            var existingNote = _context.Notes.FirstOrDefault(n => n.Id == noteId);

            if (existingNote == null)
            {
                // Gérer le cas où la note n'existe pas
                throw new ArgumentException("La note spécifiée n'existe pas.");
            }

            // Mettre à jour les propriétés de la note avec les nouvelles valeurs si elles sont fournies
            if (newContent != null)
            {
                existingNote.Content = newContent;
            }

            if (newImageUrl != null)
            {
                existingNote.ImageUrl = newImageUrl;
            }

            // Sauvegarder les modifications dans la base de données
            _context.SaveChanges();
        }

        public void DeleteNote(int noteId)
        {
            // Récupérer la note existante à partir de la base de données
            var existingNote = _context.Notes.FirstOrDefault(n => n.Id == noteId);

            if (existingNote == null)
            {
                // Gérer le cas où la note n'existe pas
                throw new ArgumentException("La note spécifiée n'existe pas.");
            }

            // Supprimer la note de la base de données
            _context.Notes.Remove(existingNote);

            // Sauvegarder les modifications dans la base de données
            _context.SaveChanges();
        }
    }
}

