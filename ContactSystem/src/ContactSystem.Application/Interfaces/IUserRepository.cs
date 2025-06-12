using ContactSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Interfaces;

public  interface IUserRepository
{
    Task<long> InsertAsync(User user);
    Task DeleteAsync(long userId);
    Task UpdateUserRoleAsync(long userId, string role);
    Task<User?> SelectByIdAsync(long userId);
    Task<User?> SelectByUserNameAsync(string userName);
    Task<ICollection<User>> SelectAllAsync(int skip, int take);
    IQueryable<User> SelectAll();

}
