using Models;
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

    public async Task<Activity> CreateAsync(Activity activityDomainModel)
    {
        await dbContext.Activities.AddAsync(activityDomainModel);
        await dbContext.SaveChangesAsync();

        return activityDomainModel;
    }

    public async Task<Activity> GetByIdAsync(Guid id)
    {
        // Get Activity from Database
        //var activity = dbContext.Activities.Find(id); //find using primary key
        return await dbContext.Activities.FirstOrDefaultAsync(x => x.Id == id); //use LINQ
    }
}
