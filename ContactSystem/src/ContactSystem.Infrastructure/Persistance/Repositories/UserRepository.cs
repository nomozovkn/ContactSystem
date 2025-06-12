using ContactSystem.Application.Interfaces;
using ContactSystem.Core.Errors;
using ContactSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContactSystem.Infrastructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<long> InsertAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.UserId;
    }
    public async Task DeleteAsync(long userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
    public  async Task UpdateUserRoleAsync(long userId, string userRole)
    {
        var user = await SelectByIdAsync(userId);
        var role = await _context.UserRoles.FirstOrDefaultAsync(x => x.RoleName == userRole);
        if (role == null)
        {
            throw new EntityNotFoundException($"Role : {userRole} not found");
        }
        user.RoleId = role.RoleId;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task<ICollection<User>> SelectAllAsync(int skip, int take)
    {
       var users=await _context.Users.Skip(skip).Take(take).ToListAsync();

        return users;
    }
    public async Task<User?> SelectByIdAsync(long userId)
    {
        var user=  await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

        return user;

    }
    public async Task<User?> SelectByUserNameAsync(string userName)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

        return user;
    }

    public IQueryable<User> SelectAll()
    {
        var users = _context.Users.AsQueryable();

        return users;
    }

}
