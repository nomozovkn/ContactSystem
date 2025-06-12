using ContactSystem.Application.DTOs;
using ContactSystem.Domain.Entities;

namespace ContactSystem.Application.Services.ContactService;

public interface IContactService
{
    Task<long> PostAsync(ContactCreateDto contact);
    Task DeleteAsync(long contactId);
    Task<ContactDto> GetByIdAsync(long contactId);
    Task<ICollection<ContactDto>> GetAllAsync(int skip, int take);
    ICollection<ContactDto> GetAll();
    Task UpdateAsync(ContactDto contactDto);
}