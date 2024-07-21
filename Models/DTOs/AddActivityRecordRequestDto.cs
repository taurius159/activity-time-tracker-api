using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;
public class AddActivityRecordRequestDto
{
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public Guid ActivityId { get; set; }
}