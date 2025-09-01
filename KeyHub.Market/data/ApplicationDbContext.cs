using KeyHub.Market.Models;
using Microsoft.EntityFrameworkCore;

namespace KeyHub.Market.data;

//dotnet add package Microsoft.EntityFrameworkCore
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// dotnet add package Microsoft.EntityFrameworkCore.Tools

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        
        public DbSet<Game>  Games { get; set; }
        
        
        
    }