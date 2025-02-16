using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Interface
{
    public interface IBuggyService
    {
        string GetSecret();
        Task<AppUser?> GetNotFoundAsync();
        Task<string> GetServerErrorAsync();
    }
}
