using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Serilog;


namespace Net9Auth.API.Database;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
    
    public DbSet<Log> Logs { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    
        base.OnModelCreating(modelBuilder);
    }
}