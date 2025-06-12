using ContactSystem.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Validators.UserValidators;

public class UserLoginValidators:AbstractValidator<UserLoginDto>
{
    public UserLoginValidators()
    {
        RuleFor(x => x.UserName)
          .NotEmpty()
          .WithMessage("UserName is required")
          .Length(3, 20)
          .WithMessage("UserName must be between 3 and 20 characters long");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .Length(8, 20)
            .WithMessage("Password must be between 8 and 20 characters long");
    }
}
