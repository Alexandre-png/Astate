using Astate.Models;
using Astate.Data;
using Microsoft.EntityFrameworkCore;

namespace Astate.Models
{
    public class UtilisateurService
    {
        private PasswordHasher _passwordHasher;
        private readonly AstateDbContext _context;

        public UtilisateurService()
        {
            _passwordHasher = new PasswordHasher();
        }
        public UtilisateurService(AstateDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        /// <summary>
        /// Récupère un utilisateur par son identifiant unique.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à récupérer.</param>
        /// <returns>L'utilisateur correspondant à l'identifiant spécifié.</returns>
        public async Task<Utilisateur> GetUtilisateurByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "L'identifiant de l'utilisateur doit être supérieur à zéro.");
            }

            var utilisateur = await _context.Utilisateurs.SingleOrDefaultAsync(u => u.Id == id);

            if (utilisateur == null)
            {
                throw new InvalidOperationException("L'utilisateur demandé n'a pas été trouvée.");
            }

            return utilisateur;
        }

        /// <summary>
        /// Crée un nouvel utilisateur dans la base de données.
        /// </summary>
        /// <param name="utilisateur">L'utilisateur à créer.</param>
        public async Task CreateUtilisateurAsync(Utilisateur utilisateur)
        {
            if (utilisateur == null)
            {
                throw new ArgumentNullException(nameof(utilisateur), "L'utilisateur ne peut pas être null.");
            }

            byte[] salt = _passwordHasher.GenerateSalt(16);

            byte[] hashedPassword = _passwordHasher.HashPassword(utilisateur.Password, salt, 10000, 32);

            utilisateur.Password = hashedPassword.ToString();
            utilisateur.Salt = salt;

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();
        }

        public bool VerifyPassword(string enteredPassword, byte[] storedPasswordHash, byte[] salt)
        {
            byte[] enteredPasswordHash = _passwordHasher.HashPassword(enteredPassword, salt, 10000, 32);

            return enteredPasswordHash.SequenceEqual(storedPasswordHash);
        }

        /// <summary>
        /// Met à jour les informations d'un utilisateur existant dans la base de données.
        /// </summary>
        /// <param name="id">L'identifiant de l'utilisateur à mettre à jour.</param>
        /// <param name="NewLastName">Le nouveau nom de famille de l'utilisateur.</param>
        /// <param name="NewFirstName">Le nouveau prénom de l'utilisateur.</param>
        /// <param name="NewEmail">Le nouveau email de l'utilisateur.</param>
        /// <param name="NewPassword">Le nouveau mot de passe de l'utilisateur.</param>
        public async Task UpdateUtilisateurAsync(int id, string NewLastName = null, string NewFirstName = null, string NewEmail = null, string NewPassword = null)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "L'identifiant de l'utilisateur doit être supérieur à zéro.");
            }

            var existingUser = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Id == id);

            if (existingUser == null)
            {
                throw new ArgumentNullException("La note spécifiée n'existe pas.");
            }

            if (!string.IsNullOrWhiteSpace(NewLastName))
            {
                existingUser.LastName = NewLastName;
            }

            if (!string.IsNullOrWhiteSpace(NewFirstName))
            {
                existingUser.FirstName = NewFirstName;
            }

            if (!string.IsNullOrWhiteSpace(NewEmail))
            {
                existingUser.Email = NewEmail;
            }

            if (!string.IsNullOrWhiteSpace(NewPassword))
            {
                existingUser.Password = NewPassword;
            }

            // Sauvegarder les modifications dans la base de données
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime un utilisateur de la base de données.
        /// </summary>
        /// <param name="idUser">L'identifiant de l'utilisateur à supprimer.</param>
        public async Task DeleteUtilisateurAsync(int idUser)
        {
            if (idUser <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(idUser), "L'identifiant de l'utilisateur doit être supérieur à zéro.");
            }

            var existingUser = await _context.Utilisateurs.SingleOrDefaultAsync(n => n.Id == idUser);

            if (existingUser == null)
            {
                throw new ArgumentException("La note spécifiée n'existe pas.");
            }

            _context.Utilisateurs.Remove(existingUser);
            await _context.SaveChangesAsync();
        }


    }

}