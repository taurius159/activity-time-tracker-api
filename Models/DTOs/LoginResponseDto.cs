using System.ComponentModel.DataAnnotations;

namespace Models.DTOs;
public class LoginResponseDto
{
    public string JwtToken { get; set; }
}
