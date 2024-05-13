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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySQL("name=DefaultConnection");
        }
    }
    }
}