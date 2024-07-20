namespace Models;

public class Activity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description {get; set; }

    // Navigation property for related ActivityRecords
    public ICollection<ActivityRecord>? ActivityRecords { get; set; }
}