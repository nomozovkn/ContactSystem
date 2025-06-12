using ContactSystem.Application.DTOs;
using ContactSystem.Application.Interfaces;
using ContactSystem.Application.Mapper;
using ContactSystem.Application.Services.Helpers.Security;
using ContactSystem.Core.Errors;
using ContactSystem.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Services.UserService;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UserCreateDto> _userCreateValidator;
    public UserService(IUserRepository userRepository,IValidator<UserCreateDto> userCreateValidator)
    {
        _userRepository = userRepository;
        _userCreateValidator = userCreateValidator;
    }

    public async Task<long> CreateAsync(UserCreateDto userCreateDto)
    {
        var validationResult =await _userCreateValidator.ValidateAsync(userCreateDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var salt = PasswordHasher.Hasher(userCreateDto.Password).Salt;
        var hashedPassword = PasswordHasher.Hasher(userCreateDto.Password).Hash;
        var user = MapUserService.ConvertToUser(userCreateDto, hashedPassword, salt);

        await _userRepository.InsertAsync(user);

        return user.UserId;

    }

    public async Task DeleteAsync(long userId, string userRole)
    {
        var user = await _userRepository.SelectByIdAsync(userId);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        if(userRole!="Admin" || userRole!="SuperAdmin")
        {
            throw new ForbiddenException("You do not have permission to delete this user");
        }

        await _userRepository.DeleteAsync(userId);

    }
    public async Task<ICollection<UserDto>> GetAllAsync(int skip, int take)
    {
        var users = await _userRepository.SelectAllAsync(skip, take);
        if (users == null || !users.Any())
        {
            throw new EntityNotFoundException("No users found");
        }
        return users.Select(u => MapUserService.ConvertToDto(u)).ToList();
    }

    public async Task<UserDto> GetByIdAsync(long userId)
    {
        var user =await _userRepository.SelectByIdAsync(userId);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        var res = MapUserService.ConvertToDto(user);

        return res;
    }

    public Task<User?> GetByUserName(string userName)
    {
       var user=_userRepository.SelectByUserNameAsync(userName);
        if (user == null)
        {
            throw new EntityNotFoundException("User not found");
        }
        return user;
    }

    public async Task UpdateUserRoleAsync(long userId, string role)=>await _userRepository.UpdateUserRoleAsync(userId, role);

}

