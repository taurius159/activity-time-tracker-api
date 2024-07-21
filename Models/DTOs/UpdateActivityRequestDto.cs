using System.ComponentModel.DataAnnotations;
namespace Models.DTOs;
public class UpdateActivityRequestDto
{
    [Required]
    [MinLength(2, ErrorMessage = "Minimum length of the name is 2")]
    [MaxLength(15, ErrorMessage = "Maximum length of the name is 15")]
    public string Name { get; set; }

    [Required]
    [MinLength(5, ErrorMessage = "Minimum length of the description is 5")]
    [MaxLength(150, ErrorMessage = "Maximum length of the description is 150")]
    public string Description { get; set; }
}
