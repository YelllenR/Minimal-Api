using Microsoft.EntityFrameworkCore;
using minimalApi.Dtos;

namespace minimalApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { 
            
        }
        public DbSet<Command> Commands => Set<Command>(); 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=minimalApiv2;Username=postgres;Password=0805");
    }
}
