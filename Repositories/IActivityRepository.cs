using Models;

namespace Api.Repositories;
public interface IActivityRepository
{
    Task<Activity> GetByIdAsync(Guid id);
    Task<Activity> CreateAsync(Activity activity);
}
