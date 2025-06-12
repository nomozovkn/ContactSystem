using ContactSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Interfaces;

public interface IContactRepository
{
    Task<long> InsertAsync(Contact contact);
    Task DeleteAsync(Contact contact);
    Task<Contact?> SelectByIdAsync(long contactId);
    Task<ICollection<Contact>> SelectAllAsync(int skip, int take);
    IQueryable<Contact> SelectAll();
    Task UpdateAsync(Contact contact);
    Task<int> SaveChangesAsync();
}
