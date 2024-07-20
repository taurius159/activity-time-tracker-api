using Models.Domains;
using Microsoft.EntityFrameworkCore;
using Api.Repositories;

namespace Repositories;
public class SqlLiteActivityRepository : IActivityRepository
{
    private readonly AppDbContext dbContext;
    public SqlLiteActivityRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<Activity>> GetAllAsync()
    {
        return await dbContext.Activities.ToListAsync();
    }

    public async Task<Activity> GetByIdAsync(Guid id)
    {
        // Get Activity from Database
        //var activity = dbContext.Activities.Find(id); //find using primary key
        return await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id); //use LINQ
    }

    public async Task<Activity> CreateAsync(Activity activityDomainModel)
    {
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
