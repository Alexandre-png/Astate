using Microsoft.EntityFrameworkCore;
using Astate.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Astate.Data
{
    public class AstateDbContext : IdentityDbContext<IdentityUser>
    {
        public AstateDbContext(DbContextOptions<AstateDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasOne(n => n.Owner)
                .WithMany()
                .HasForeignKey(n => n.IdOwner)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.UploadedBy)
                .WithMany()
                .HasForeignKey(i => i.UploadedById)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}