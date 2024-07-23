using Microsoft.AspNetCore.Identity;

namespace Models.Domains;

public class Activity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description {get; set; }
    public string UserId { get; set; }

    // Navigation properties
    public ICollection<ActivityRecord> ActivityRecords { get; } = new List<ActivityRecord>();
}