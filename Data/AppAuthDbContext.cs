using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;
public class AppAuthDbContext : IdentityDbContext
{
    public AppAuthDbContext(DbContextOptions<AppAuthDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var userRoleId = "7dce9596-474b-45d5-9c7d-82dcee035f40";

        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = userRoleId,
                ConcurrencyStamp = userRoleId,
                Name = "User",
                NormalizedName = "User".ToUpper()
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}
