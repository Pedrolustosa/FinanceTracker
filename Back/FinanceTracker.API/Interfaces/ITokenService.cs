using FinanceTracker.API.Entities;

namespace FinanceTracker.API.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
