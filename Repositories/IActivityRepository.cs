using Models.Domains;

namespace Api.Repositories;
public interface IActivityRepository
{
    Task<List<Activity>> GetAllAsync();
    Task<Activity?> GetByIdAsync(Guid id);
    Task<Activity?> CreateAsync(Activity activity);
    Task<Activity?> UpdateAsync(Guid id, Activity activity);
    Task<Activity?> DeleteAsync(Guid id);
}
