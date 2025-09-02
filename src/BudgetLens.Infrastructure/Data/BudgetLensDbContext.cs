using Microsoft.EntityFrameworkCore;
using BudgetLens.Infrastructure.EventStore;

namespace BudgetLens.Infrastructure.Data;

/// <summary>
/// Entity Framework DbContext for Budget Lens with PostgreSQL and event sourcing support.
/// Uses snake_case naming convention for PostgreSQL compatibility.
/// </summary>
public class BudgetLensDbContext : DbContext
{
    public BudgetLensDbContext(DbContextOptions<BudgetLensDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Event store for all domain events
    /// </summary>
    public DbSet<StoredEvent> Events { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure StoredEvent entity
        modelBuilder.Entity<StoredEvent>(entity =>
        {
            // Table name in snake_case
            entity.ToTable("events");

            // Primary key
            entity.HasKey(e => e.EventId)
                  .HasName("pk_events");

            // Properties with snake_case column names
            entity.Property(e => e.EventId)
                  .HasColumnName("event_id")
                  .IsRequired();

            entity.Property(e => e.AggregateId)
                  .HasColumnName("aggregate_id")
                  .IsRequired();

            entity.Property(e => e.AggregateType)
                  .HasColumnName("aggregate_type")
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(e => e.EventType)
                  .HasColumnName("event_type")
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(e => e.EventData)
                  .HasColumnName("event_data")
                  .HasColumnType("jsonb")
                  .IsRequired();

            entity.Property(e => e.Metadata)
                  .HasColumnName("metadata")
                  .HasColumnType("jsonb");

            entity.Property(e => e.Version)
                  .HasColumnName("version")
                  .IsRequired();

            entity.Property(e => e.OccurredAt)
                  .HasColumnName("occurred_at")
                  .HasColumnType("timestamp with time zone")
                  .IsRequired();

            entity.Property(e => e.UserId)
                  .HasColumnName("user_id");

            // Indexes for performance
            entity.HasIndex(e => e.AggregateId)
                  .HasDatabaseName("idx_events_aggregate_id");

            entity.HasIndex(e => new { e.AggregateId, e.Version })
                  .IsUnique()
                  .HasDatabaseName("idx_events_aggregate_version");

            entity.HasIndex(e => e.AggregateType)
                  .HasDatabaseName("idx_events_aggregate_type");

            entity.HasIndex(e => e.EventType)
                  .HasDatabaseName("idx_events_event_type");

            entity.HasIndex(e => e.OccurredAt)
                  .HasDatabaseName("idx_events_occurred_at");
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // Configure PostgreSQL with snake_case naming convention
        if (optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null);
            });
        }
    }
}