using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Infra.Interfaces
{
    public interface IBuggyRepository
    {
        Task<AppUser?> GetUserByIdAsync(int id);
    }
}
