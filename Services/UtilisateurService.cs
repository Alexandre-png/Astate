using Astate.Models;
using Astate.Data;

namespace Astate.Models
{
    public class UtilisateurService
    {

        private readonly AstateDbContext _context;

        public UtilisateurService(AstateDbContext context)
        {
            _context = context;
        }

        public void CreateUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            _context.SaveChanges();
        }

        public Utilisateur GetUtilisateurById(int id)
        {
            var utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.Id == id);

            if (utilisateur == null)
            {
                throw new InvalidOperationException("L'utilisateur demandé n'a pas été trouvée.");
            }

            return utilisateur;
        }

        public void UpdateUtilisateur(int id, string NewLastName = null, string NewFirstName = null, string NewEmail = null, string NewPassword = null)
        {
            // Récupérer la note existante à partir de la base de données
            var existingUser = _context.Utilisateurs.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                // Gérer le cas où la note n'existe pas
                throw new ArgumentException("La note spécifiée n'existe pas.");
            }

            // Mettre à jour les propriétés de la note avec les nouvelles valeurs si elles sont fournies
            if (NewLastName != null)
            {
                existingUser.LastName = NewLastName;
            }

            if (NewFirstName != null)
            {
                existingUser.FirstName = NewFirstName;
            }

            if (NewEmail != null)
            {
                existingUser.Email = NewEmail;
            }

            if (NewPassword != null)
            {
                existingUser.Password = NewPassword;
            }

            // Sauvegarder les modifications dans la base de données
            _context.SaveChanges();
        }

        public void DeleteUtilisateur(int idUser)
        {
            // Récupérer la note existante à partir de la base de données
            var existingUser = _context.Utilisateurs.FirstOrDefault(n => n.Id == idUser);

            if (existingUser == null)
            {
                // Gérer le cas où la note n'existe pas
                throw new ArgumentException("La note spécifiée n'existe pas.");
            }

            // Supprimer la note de la base de données
            _context.Utilisateurs.Remove(existingUser);

            // Sauvegarder les modifications dans la base de données
            _context.SaveChanges();
        }


    }

}