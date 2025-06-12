using ContactSystem.Application.DTOs;
using ContactSystem.Application.Interfaces;
using ContactSystem.Application.Mapper;
using ContactSystem.Core.Errors;
using ContactSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Services.UserRoleService;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IUserRepository _userRepository;
    public UserRoleService(IUserRoleRepository roleRepository, IUserRepository userRepository)
    {
        _userRoleRepository = roleRepository;
        _userRepository = userRepository;
    }

    public async  Task<List<RoleDto>> GetAllRolesAsync()
    {
       var roles = await _userRoleRepository.GetAllRolesAsync();
        return roles.Select(role => new RoleDto
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName
        }).ToList();
    }

    public async Task<ICollection<UserDto>> GetAllUsersByRoleAsync(string role)
    {
        var users = await _userRoleRepository.GetAllUsersByRoleAsync(role);

        if (users is null || users.Count == 0)
        {
            throw new EntityNotFoundException($"No users found for role: {role}");
        }
        var userDtos=users.Select(u=>MapUserService.ConvertToDto(u)).ToList();

        return userDtos;
    }

    public Task<long> GetRoleIdAsync(string role)
    {
       var res = _userRoleRepository.GetRoleIdAsync(role);
        if (res is null)
        {
            throw new EntityNotFoundException(role);
        }
        return res;
    }
}
