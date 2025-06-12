using ContactSystem.Application.Interfaces;
using ContactSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactSystem.Infrastructure.Persistance.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly AppDbContext _context;
    public ContactRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<long> InsertAsync(Contact contact)
    {
        //    await _context.Contacts.AddAsync(contact);
        //    await _context.SaveChangesAsync();

        //    return contact.ContactId;
        try
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            return contact.ContactId;
        }
        catch (Exception ex)
        {
            // Xatolik haqida log yoki console ga chiqarish
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    public async Task DeleteAsync(Contact contact)
    {
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Contact> SelectAll()
    {

        return _context.Contacts.AsQueryable();
    }

    public async Task<Contact> SelectByIdAsync(long contactId)
    {
        var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.ContactId == contactId);

        return contact;
    }
    public async Task<ICollection<Contact>> SelectAllAsync(int skip, int take)
    {
        var contacts=await _context.Contacts.Skip(skip).Take(take).ToListAsync();

        return contacts;
    }
    public Task UpdateAsync(Contact contact)
    {
        _context.Contacts.Update(contact);
        return _context.SaveChangesAsync();
    }
    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    
}
