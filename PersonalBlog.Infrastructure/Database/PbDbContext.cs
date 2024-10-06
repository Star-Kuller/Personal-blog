using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalBlog.Core.Interfaces;
using PersonalBlog.Domain;
using PersonalBlog.Domain.Common;

namespace PersonalBlog.Infrastructure.Database;

public class PbDbContext(DbContextOptions options) : DbContext(options), IPbDbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        SetupUtcDateTimes(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PbDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        SetupUpdatableDates();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetupUpdatableDates();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetupUpdatableDates();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SetupUpdatableDates();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private static void SetupUtcDateTimes(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var properties = entityType
                .GetProperties()
                .ToList();


#pragma warning disable EF1001
            var entityTypeBuilder = new EntityTypeBuilder(entityType);
#pragma warning restore EF1001
            foreach (var property in properties)
            {
                if (property.ClrType == typeof(DateTime))
                {
                    entityTypeBuilder
                        .Property<DateTime>(property.Name)
                        .HasConversion(
                            v => v.ToUniversalTime(),
                            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    entityTypeBuilder
                        .Property<DateTime?>(property.Name)
                        .HasConversion(
                            v => v.HasValue ? v.Value.ToUniversalTime() : v,
                            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);
                }
            }
        }
    }


    private void SetupUpdatableDates()
    {
        var entities = ChangeTracker
            .Entries()
            .Where(x => x is { Entity: UpdatableEntity, State: EntityState.Added });

        foreach (var entity in entities)
        {
            if (entity.State == EntityState.Added)
            {
                ((UpdatableEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
            }
        }
    }
}