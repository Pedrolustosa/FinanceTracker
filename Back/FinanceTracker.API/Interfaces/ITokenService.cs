using FinanceTracker.API.Entities;

namespace FinanceTracker.API.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}
