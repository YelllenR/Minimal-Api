using Microsoft.EntityFrameworkCore;
using minimalApi.Models;

namespace minimalApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { 
            
        }
        public DbSet<Command> Commands => Set<Command>(); 

        //Configuration for postgres
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=minimalApiv2;Username=postgres;Password=0805");
    }
}
