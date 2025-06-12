using ContactSystem.Application.DTOs;
using ContactSystem.Application.Interfaces;
using ContactSystem.Application.Mapper;
using ContactSystem.Core.Errors;
using FluentValidation;

namespace ContactSystem.Application.Services.ContactService;

public class ContactService : IContactService
{
    private readonly IContactRepository _contactRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<ContactCreateDto> _contactCreateValidator;
    private readonly IValidator<ContactDto> _contactUpdateValidator;
    public ContactService(
        IContactRepository contactRepository,
        IUserRepository userRepository,
        IValidator<ContactCreateDto> contactCreateValidator,
        IValidator<ContactDto> contactUpdateValidator)
    {
        _contactRepository = contactRepository;
        _userRepository = userRepository;
        _contactCreateValidator = contactCreateValidator;
        _contactUpdateValidator = contactUpdateValidator;
    }
    public async Task<long> PostAsync(ContactCreateDto contactCreateDto)
    {
        var validationResult = await _contactCreateValidator.ValidateAsync(contactCreateDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        //var user = await _userRepository.SelectByIdAsync(contactCreateDto.UserId);
        //if (user == null)
        //{
        //    throw new EntityNotFoundException($"User with ID {contactCreateDto.UserId} not found.");
        //}

        var contact = MapContactService.ConvertToEntity(contactCreateDto);
        await _contactRepository.InsertAsync(contact);


        return contact.ContactId;
    }
    public async Task DeleteAsync(long contactId)
    {
        var contact = await _contactRepository.SelectByIdAsync(contactId);
        if (contact == null)
        {
            throw new KeyNotFoundException($"Contact with ID {contactId} not found.");
        }
        await _contactRepository.DeleteAsync(contact);
    }
    public async Task UpdateAsync(ContactDto contactDto)
    {
        var existingContact = await _contactRepository.SelectByIdAsync(contactDto.ContactId);
        if (existingContact is null)
        {
            throw new KeyNotFoundException($"Contact with ID {contactDto.ContactId} not found.");
        }
        var validatedResult = await _contactUpdateValidator.ValidateAsync(contactDto);
        if (!validatedResult.IsValid)
        {
            throw new ValidationException(validatedResult.Errors);
        }
        var updatedContact = MapContactService.ConvertToEntity(contactDto); 

        await _contactRepository.UpdateAsync(updatedContact);
    }
    public async Task<ContactDto> GetByIdAsync(long contactId)
    {
        var contact = await _contactRepository.SelectByIdAsync(contactId);
        if (contact == null)
        {
            throw new KeyNotFoundException($"Contact with ID {contactId} not found.");
        }

        var contactDto = MapContactService.ConvertToDto(contact);

        return contactDto;
    }

    public async Task<ICollection<ContactDto>> GetAllAsync(int skip, int take)
    {
      var contacts = await _contactRepository.SelectAllAsync(skip, take);
        if (contacts == null || !contacts.Any())
        {
            return new List<ContactDto>();
        }
        var contactDtos = contacts.Select(c => MapContactService.ConvertToDto(c)).ToList();
        return contactDtos;


    }
    public ICollection<ContactDto> GetAll()
    {
       var contacts = _contactRepository.SelectAll();
        if (contacts == null || !contacts.Any())
        {
            return new List<ContactDto>();
        }
        var contactDtos = contacts.Select(c=>MapContactService.ConvertToDto(c)).ToList();

        return contactDtos;
    }

    
}
