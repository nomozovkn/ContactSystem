using ContactSystem.Application.DTOs;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Services.UserRoleService;

public interface IUserRoleService
{
    Task<ICollection<UserDto>> GetAllUsersByRoleAsync(string role);
    Task<List<RoleDto>> GetAllRolesAsync();
    Task<long> GetRoleIdAsync(string role);
}