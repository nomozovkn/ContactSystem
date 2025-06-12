using ContactSystem.Application.DTOs;
using ContactSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Interfaces;

public interface IUserRoleRepository
{
    Task<ICollection<User>> GetAllUsersByRoleAsync(string role);
    Task<List<UserRole>> GetAllRolesAsync();
    Task<long> GetRoleIdAsync(string role);
}
