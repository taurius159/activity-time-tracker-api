using Models.Domains;
using Microsoft.EntityFrameworkCore;
using Data;
using Microsoft.AspNetCore.Identity;

namespace Repositories;
public class SqlLiteActivityRepository : IActivityRepository
{
    private readonly AppDbContext dbContext;
    private readonly UserManager<IdentityUser> userManager;

    public SqlLiteActivityRepository(AppDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
    }

    public async Task<List<Activity>> GetAllByUserIdAsync(string userId)
    {
        return await dbContext.Activities
                                    .Where(a => a.UserId == userId)
                                    .Include(a => a.ActivityRecords)
                                    .ToListAsync();
    }

    public async Task<Activity?> GetByActivityIdAndUserIdAsync(Guid id, string userId)
    {
        // Get Activity from Database
        //var activity = dbContext.Activities.Find(id); //find using primary key
        return await dbContext.Activities.Where(a => a.UserId == userId)
                                        .FirstOrDefaultAsync(x => x.Id == id); //use LINQ
    }

    public async Task<Activity?> CreateAsync(Activity activityDomainModel)
    {
        // Manually check if the user exists
        var user = await userManager.FindByIdAsync(activityDomainModel.UserId);
        if (user == null)
        {
            throw new InvalidOperationException("User does not exist.");
        }

        await dbContext.Activities.AddAsync(activityDomainModel);
        await dbContext.SaveChangesAsync();

        return activityDomainModel;
    }

    public async Task<Activity?> UpdateAsync(Guid id, Activity activity)
    {
        // check if activity exists
        var existingActivity = await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id);

        if (existingActivity == null)
        {
            return null;
        }

        //map DTO to domain model and save in db
        existingActivity.Name = activity.Name;
        existingActivity.Description = activity.Description;

        await dbContext.SaveChangesAsync();
        return existingActivity;
    }

    public async Task<Activity?> DeleteAsync(Guid id)
    {
        // check if activity exists
        var activityDomainModel = await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id);

        if (activityDomainModel == null)
        {
            return null;
        }

        //delete region
        dbContext.Activities.Remove(activityDomainModel);
        await dbContext.SaveChangesAsync();
        return activityDomainModel;
    }
}
