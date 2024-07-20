using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;
public class ActivityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description {get; set; }

    // Navigation property for related ActivityRecords
    public ICollection<ActivityRecord>? ActivityRecords { get; set; }
}
