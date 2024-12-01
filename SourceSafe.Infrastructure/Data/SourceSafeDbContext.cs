using Microsoft.EntityFrameworkCore;
using SourceSafe.Domain.Entities;

namespace SourceSafe.Infrastructure.Data;

public class SourceSafeDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=SourceSafe;Trusted_Connection=true;TrustServerCertificate=true;");
    }
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<GroupUser> GroupUsers { get; set; } = null!;
    public DbSet<Domain.Entities.File> Files { get; set; } = null!;
    public DbSet<Backup> Backups { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Group>()
            .HasOne(g => g.Admin)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        var adminRole = new Role()
        {
            Id = 1,
            Name = "Admin"
        };
        builder.Entity<Role>().HasData(adminRole);
        builder.Entity<Role>().HasData(
            new Role
            {
                Id = 2,
                Name = "User"
            });
        var admin = new User
        {
            Id = 1,
            Name = "Admin",
            Email = "Admin@gmail.com",
            Password = "1234567",
            RoleId = adminRole.Id
        };
        builder.Entity<User>().HasData(admin);
        base.OnModelCreating(builder);
    }
}
