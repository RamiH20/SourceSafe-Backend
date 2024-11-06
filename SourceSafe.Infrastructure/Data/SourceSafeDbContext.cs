using Microsoft.EntityFrameworkCore;
using SourceSafe.Domain.Entities;

namespace SourceSafe.Infrastructure.Data;

public class SourceSafeDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=SourceSafe;Trusted_Connection=true;TrustServerCertificate=true;");
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupUsers> GroupUsers { get; set; }
    public DbSet<Domain.Entities.File> Files { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroupUsers>().HasNoKey();
        base.OnModelCreating(modelBuilder);
    }
}
