namespace Models.Domains;

public class Activity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description {get; set; }

    // Navigation property for related ActivityRecords
    public ICollection<ActivityRecord> ActivityRecords { get; } = new List<ActivityRecord>();
}