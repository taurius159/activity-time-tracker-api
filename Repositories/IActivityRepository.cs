using Models.Domains;

namespace Repositories;
public interface IActivityRepository
{
    Task<List<Activity>> GetAllByUserIdAsync(string userId);
    Task<Activity?> GetByActivityIdAndUserIdAsync(Guid id, string userId);
    Task<Activity?> CreateAsync(Activity activity);
    Task<Activity?> UpdateAsync(Guid id, Activity activity);
    Task<Activity?> DeleteAsync(Guid id);
}
