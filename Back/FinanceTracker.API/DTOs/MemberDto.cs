﻿namespace FinanceTracker.API.DTOs;

public class MemberDto
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public int Age { get; set; }
    public string PhotoUrl { get; set; }
    public required string KnownAs { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public List<PhotoDto>? Photos { get; set; } = [];
}
