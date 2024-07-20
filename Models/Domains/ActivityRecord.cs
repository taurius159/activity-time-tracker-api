namespace Models.Domains;

public class ActivityRecord
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

     // Foreign key for Activity
    public int ActivityId { get; set; }

    // Navigation property for the related Activity
    public Activity Activity { get; set; }
}