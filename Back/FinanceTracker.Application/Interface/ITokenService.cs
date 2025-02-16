using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);
}
