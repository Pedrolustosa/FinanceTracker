using FinanceTracker.Application.Interface;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infra.Interfaces;

namespace FinanceTracker.Application.Services
{
    public class BuggyService(IBuggyRepository buggyRepository) : IBuggyService
    {
        private readonly IBuggyRepository _buggyRepository = buggyRepository;

        public string GetSecret()
        {
            return "secret text";
        }

        public async Task<AppUser?> GetNotFoundAsync()
        {
            return await _buggyRepository.GetUserByIdAsync(-1);
        }

        public async Task<string> GetServerErrorAsync()
        {
            var user = await _buggyRepository.GetUserByIdAsync(-1);
            return user.ToString();
        }
    }
}
