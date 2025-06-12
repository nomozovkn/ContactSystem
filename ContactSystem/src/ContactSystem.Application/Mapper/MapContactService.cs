using ContactSystem.Application.DTOs;
using ContactSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Mapper;

public static class MapContactService
{
    public static Contact ConvertToEntity(ContactCreateDto contactCreateDto)
    {
        return new Contact
        {
            Name = contactCreateDto.Name,
            Email = contactCreateDto.Email,
            Phone = contactCreateDto.PhoneNumber,
            Address = contactCreateDto.Address
        };
    }
    public static ContactDto ConvertToDto(Contact contact)
    {
        return new ContactDto
        {
           
            ContactId = contact.ContactId,
            Name = contact.Name,
            Email = contact.Email,
            PhoneNumber = contact.Phone,
            Address = contact.Address
        };
    }
    public static Contact ConvertToEntity(ContactDto contactDto)
    {
        return new Contact
        {
            ContactId = contactDto.ContactId,
            Name = contactDto.Name,
            Email = contactDto.Email,
            Phone = contactDto.PhoneNumber,
            Address = contactDto.Address
        };
    }
}
