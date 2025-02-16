using FinanceTracker.Application.DTOs;

namespace FinanceTracker.Application.Interface
{
    public interface IAdminService
    {
        Task<IList<string>?> EditRoles(string username, string roles);
    }
}
