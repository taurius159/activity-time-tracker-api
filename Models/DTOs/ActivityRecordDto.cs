using System.ComponentModel.DataAnnotations;
using Models.Domains;

namespace Models.DTOs;
public class ActivityRecordDto
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

     // Foreign key for Activity
    public Guid ActivityId { get; set; }

    // Navigation property for the related Activity
    public Activity Activity { get; set; }
}
