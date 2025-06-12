using ContactSystem.Application.Interfaces;
using ContactSystem.Core.Errors;
using ContactSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Infrastructure.Persistance.Repositories;

public class RoleRepository : IUserRoleRepository
{
    private readonly AppDbContext _context;
    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }
    public async  Task<List<UserRole>> GetAllRolesAsync()
    {
        var roles = await _context.UserRoles.ToListAsync();
        return roles;
    }

    public async Task<ICollection<User>> GetAllUsersByRoleAsync(string role)
    {
        var foundRole = await _context.UserRoles.Include(u => u.Users).FirstOrDefaultAsync(u => u.RoleName == role);
        if (foundRole is null)
        {
            throw new EntityNotFoundException(role);
        }
        return foundRole.Users;

    }

    public async Task<long> GetRoleIdAsync(string role)
    {
       var res=await _context.UserRoles.FirstOrDefaultAsync(r => r.RoleName == role);
        if (role is null)
        {
            throw new EntityNotFoundException(role);
        }
        return res.RoleId;
    }
}
