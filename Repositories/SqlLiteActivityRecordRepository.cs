using Models.Domains;
using Microsoft.EntityFrameworkCore;
using Data;

namespace Repositories;
public class SqlLiteActivityRecordRepository : IActivityRecordRepository
{
    private readonly AppDbContext dbContext;
    public SqlLiteActivityRecordRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<ActivityRecord>> GetAllAsync()
    {
        return await dbContext.ActivityRecords.ToListAsync();
    }

    public async Task<ActivityRecord?> GetByIdAsync(Guid id)
    {
        // Get Activity from Database
        //var activity = dbContext.Activities.Find(id); //find using primary key
        return await dbContext.ActivityRecords.FirstOrDefaultAsync(x => x.Id == id); //use LINQ
    }

    public async Task<ActivityRecord?> CreateAsync(ActivityRecord activityRecordDomainModel)
    {
        await dbContext.ActivityRecords.AddAsync(activityRecordDomainModel);
        await dbContext.SaveChangesAsync();

        return activityRecordDomainModel;
    }

    public async Task<ActivityRecord?> DeleteAsync(Guid id)
    {
        // check if activity exists
        var activityRecordDomainModel = await dbContext.ActivityRecords.FirstOrDefaultAsync(x => x.Id == id);

        if (activityRecordDomainModel == null)
        {
            return null;
        }

        //delete region
        dbContext.ActivityRecords.Remove(activityRecordDomainModel);
        await dbContext.SaveChangesAsync();
        return activityRecordDomainModel;
    }
}
