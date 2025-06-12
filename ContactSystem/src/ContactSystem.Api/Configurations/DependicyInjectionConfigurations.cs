using ContactSystem.Application.DTOs;
using ContactSystem.Application.Interfaces;
using ContactSystem.Application.Services.AuthService;
using ContactSystem.Application.Services.ContactService;
using ContactSystem.Application.Services.TokenService;
using ContactSystem.Application.Services.UserRoleService;
using ContactSystem.Application.Services.UserService;
using ContactSystem.Application.Validators.ContactValidators;
using ContactSystem.Application.Validators.UserValidators;
using ContactSystem.Infrastructure.Persistance.Repositories;
using FluentValidation;

namespace ContactSystem.Api.Configurations;

public static class DependicyInjectionConfigurations
{
    public static void Configure(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IContactRepository, ContactRepository>();
        builder.Services.AddScoped<IContactService, ContactService>();

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<IValidator<UserCreateDto>, UserCreateValidators>();
        builder.Services.AddScoped<IValidator<UserLoginDto>, UserLoginValidators>();

        builder.Services.AddScoped<IValidator<ContactCreateDto>, ContactCreateValidators>();
        builder.Services.AddScoped<IValidator<ContactDto>, ContactUpdateValidators>();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITokenService, TokenService>();

        builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        builder.Services.AddScoped<IUserRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IUserRoleService, UserRoleService>();


    }
}
