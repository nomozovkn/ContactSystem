using ContactSystem.Application.DTOs;
using ContactSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactSystem.Application.Mapper;

public static class MapUserService
{
    public static User ConvertToUser(UserCreateDto dto, string hashedPassword, string salt)
    {
        return new User
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            UserName = dto.UserName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Password = hashedPassword,
            Salt = salt,
            Role = new UserRole
            {
                RoleName = "User",
                Description = "Default role for new users",
            }
        };
    }
    public static UserDto ConvertToDto(User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role=new RoleDto()
            {
                RoleId = user.Role.RoleId,
                RoleName = user.Role.RoleName
            }
        };
    }
    public static User ConvertToEntity(UserDto userDto)
    {
        return new User
        {
            UserId = userDto.UserId,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            UserName = userDto.UserName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            Role = new UserRole
            {
                RoleId = userDto.Role.RoleId,
                RoleName = userDto.Role.RoleName
            }
        };
    }
}
