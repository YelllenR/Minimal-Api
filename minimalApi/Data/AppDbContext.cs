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
            
    }
}
