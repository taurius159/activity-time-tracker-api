using Models.Domains;

namespace Repositories;
public interface IActivityRecordRepository
{
    Task<List<ActivityRecord>> GetAllByUserAsync(string userId);
    Task<ActivityRecord?> GetByRecordIdAndUserIdAsync(Guid id, string userId);
    Task<ActivityRecord?> CreateAsync(ActivityRecord activity);
    Task<ActivityRecord?> DeleteAsync(Guid id, string userId);
}
