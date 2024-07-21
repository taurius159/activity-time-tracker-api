using Microsoft.EntityFrameworkCore;
using Models.Domains;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Activity> Activities { get; set; }
    public DbSet<ActivityRecord> ActivityRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>()
            .HasMany(a => a.ActivityRecords)
            .WithOne(ar => ar.Activity)
            .HasForeignKey(ar => ar.ActivityId)
            .IsRequired();
    }
}
