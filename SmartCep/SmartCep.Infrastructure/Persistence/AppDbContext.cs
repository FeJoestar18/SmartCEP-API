using Microsoft.EntityFrameworkCore;
using SmartCep.Infrastructure.Persistence.Entities;

namespace SmartCep.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CodeEntity> PostalCodes => Set<CodeEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CodeEntity>(entity =>
        {
            entity.HasKey(e => e.PostalCode);
            entity.HasIndex(e => e.PostalCode).IsUnique();
        });
    }
}

