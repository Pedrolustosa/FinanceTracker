using FinanceTracker.API.Extensions;

namespace FinanceTracker.API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    public DateOnly DateOfBirth { get; set; }
    public string KnownAs { get; set; }
    public string Gender { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public List<Photo> Photos { get; set; } = [];
    public List<Message> MessagesSent { get; set; } = [];
    public List<Message> MessagesReceived { get; set; } = [];

    public int GetAge()
    {
        return DateOfBirth.CalculateAge();
    }
}