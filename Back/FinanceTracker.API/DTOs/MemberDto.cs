namespace FinanceTracker.API.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int Age { get; set; }
    public string PhotoUrl { get; set; }
    public required string Knows { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string? Gender { get; set; }
    public string? Introdution { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public List<PhotoDto>? Photos { get; set; } = [];
}
