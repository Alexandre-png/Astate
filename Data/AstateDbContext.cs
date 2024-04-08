using Microsoft.EntityFrameworkCore;
using Astate.Models;

namespace Astate.Data
{
    public class AstateDbContext : DbContext
    {
        public AstateDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Vous pouvez ajouter ici des configurations de modèle spécifiques si nécessaire
        }
    }
}