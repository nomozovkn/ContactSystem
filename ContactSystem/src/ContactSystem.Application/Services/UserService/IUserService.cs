using ContactSystem.Application.DTOs;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Services.UserService;

public interface IUserService
{
    Task<long> CreateAsync(UserCreateDto userCreateDto);
    Task DeleteAsync(long userId, string userRole);
    Task<UserDto> GetByIdAsync(long userId);
    Task<ICollection<UserDto>> GetAllAsync(int skip, int take);
    Task<User?> GetByUserName(string userName);
    Task UpdateUserRoleAsync(long userId, string role);


}