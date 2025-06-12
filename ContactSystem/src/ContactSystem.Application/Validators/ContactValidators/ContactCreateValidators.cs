using ContactSystem.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Validators.ContactValidators;

public class ContactCreateValidators : AbstractValidator<ContactCreateDto>
{
    public ContactCreateValidators()
    {
        RuleFor(x => x.Name)
            .Length(2, 50)
            .When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage("FirstName must be between 2 and 50 characters long");       

        RuleFor(x => x.Email)
             .EmailAddress()
             .When(x => !string.IsNullOrWhiteSpace(x.Email))
             .WithMessage("Invalid email format");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required")
            .Matches(@"^\+?[1-9]\d{1,14}$")
            .WithMessage("Invalid phone number format");
    }
}
