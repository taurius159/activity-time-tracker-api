using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;
public class AddActivityRequestDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Minimum length of the code is 2")]
    [MaxLength(15, ErrorMessage = "Maximum length of the code is 15")]
    public string Name { get; set; }

    [MinLength(5, ErrorMessage = "Minimum length of the name is 5")]
    [MaxLength(150, ErrorMessage = "Maximum length of the name is 150")]
    public string? Description {get; set; }
}