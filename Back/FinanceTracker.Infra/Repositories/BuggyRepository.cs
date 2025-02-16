using FinanceTracker.Domain.Entities;
using FinanceTracker.Infra.Data;
using FinanceTracker.Infra.Interfaces;

namespace FinanceTracker.Infra.Repositories
{
    public class BuggyRepository(DataContext context) : IBuggyRepository
    {
        private readonly DataContext _context = context;

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
