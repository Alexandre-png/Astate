using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Astate.Data
{
    public class AstateDbContextFactory : IDesignTimeDbContextFactory<AstateDbContext>
    {
        public AstateDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AstateDbContext>();

            // Configure the DbContextOptionsBuilder with the connection string
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySQL(connectionString);

            return new AstateDbContext(optionsBuilder.Options);
        }
    }
}
