using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.DTOs;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [StringLength(12, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string? DateOfBirth { get; set; }

    [Required]
    public string KnownAs { get; set; }
}
