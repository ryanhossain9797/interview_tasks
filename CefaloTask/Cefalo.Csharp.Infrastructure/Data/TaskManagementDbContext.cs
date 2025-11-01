using Microsoft.EntityFrameworkCore;
using Cefalo.Csharp.Core.Entities;

namespace Cefalo.Csharp.Infrastructure.Data;

public class TaskManagementDbContext : DbContext
{
    public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options) : base(options)
    {
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Deleted).HasDefaultValue(false);

            entity.HasOne(e => e.User)
                  .WithMany(u => u.Tickets)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Deleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "John Doe", Email = "john.doe@example.com", Deleted = false },
            new User { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com", Deleted = false }
        );

        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                Id = 1,
                Title = "Complete project documentation",
                Status = TicketStatus.Todo,
                CreatedAt = new DateTime(2024, 6, 27, 0, 0, 0, DateTimeKind.Utc),
                UserId = 1,
                Deleted = false
            },
            new Ticket
            {
                Id = 2,
                Title = "Review code changes",
                Status = TicketStatus.InProgress,
                CreatedAt = new DateTime(2024, 6, 26, 0, 0, 0, DateTimeKind.Utc),
                UserId = 2,
                Deleted = false
            }
        );
    }
}