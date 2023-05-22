using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CleanArchitecture.Persistence.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Permissions> Permissions { get; set; }

    public DbSet<PermissionTypes> PermissionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PermissionTypes>()
            .HasData(
                new PermissionTypes
                {
                    Id = 1,
                    Description = "PermissionType1"
                },
                new PermissionTypes
                {
                    Id = 2,
                    Description = "PermissionType2"
                },
                new PermissionTypes
                {
                    Id = 3,
                    Description = "PermissionType3"
                }
            );
    }
}