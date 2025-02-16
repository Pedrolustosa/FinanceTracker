using FinanceTracker.Application.DTOs;
using FinanceTracker.Application.Interface;
using FinanceTracker.Infra.Interfaces;

namespace FinanceTracker.Application.Services
{
    public class AdminService(IAdminRepository adminRepository) : IAdminService
    {
        private readonly IAdminRepository _adminRepository = adminRepository;

        public async Task<IList<string>?> EditRoles(string username, string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
                return null;

            var selectedRoles = roles.Split(",").Select(r => r.Trim()).ToArray();

            var user = await _adminRepository.FindByNameAsync(username);
            if (user == null) return null;

            var currentRoles = await _adminRepository.GetRolesAsync(user);

            var rolesToAdd = selectedRoles.Except(currentRoles);
            var rolesToRemove = currentRoles.Except(selectedRoles);

            var addResult = await _adminRepository.AddToRolesAsync(user, rolesToAdd);
            if (!addResult.Succeeded)
                return null;

            var removeResult = await _adminRepository.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
                return null;

            return await _adminRepository.GetRolesAsync(user);
        }
    }
}
