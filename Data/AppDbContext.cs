using Microsoft.EntityFrameworkCore;
using Models.Domains;

namespace Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityRecord> ActivityRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.UserId).IsRequired();

            entity.HasMany(a => a.ActivityRecords)
                  .WithOne(ar => ar.Activity)
                  .HasForeignKey(ar => ar.ActivityId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ActivityRecord>(entity =>
        {
            entity.HasKey(ar => ar.Id);
            entity.Property(ar => ar.UserId).IsRequired();

            entity.HasOne(ar => ar.Activity)
                  .WithMany(a => a.ActivityRecords)
                  .HasForeignKey(ar => ar.ActivityId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
