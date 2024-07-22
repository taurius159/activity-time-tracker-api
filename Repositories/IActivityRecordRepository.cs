using Models.Domains;

namespace Repositories;
public interface IActivityRecordRepository
{
    Task<List<ActivityRecord>> GetAllAsync();
    Task<ActivityRecord?> GetByIdAsync(Guid id);
    Task<ActivityRecord?> CreateAsync(ActivityRecord activity);
    Task<ActivityRecord?> DeleteAsync(Guid id);
}
