using ContactSystem.Application.DTOs;
using ContactSystem.Application.Interfaces;
using ContactSystem.Application.Mapper;
using ContactSystem.Application.Services.Helpers.Security;
using ContactSystem.Application.Services.TokenService;
using ContactSystem.Core.Errors;
using ContactSystem.Domain.Entities;
using FluentValidation;
using System.Security.Claims;

namespace ContactSystem.Application.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IValidator<UserCreateDto> _userValidator;
    private readonly IValidator<UserLoginDto> _userLoginValidator;
    private readonly IUserRoleRepository _roleRepository;
    public AuthService(IRefreshTokenRepository refreshTokenRepository, IUserRepository userrepository, ITokenService token, IValidator<UserCreateDto> userValidator, IValidator<UserLoginDto> userLoginValidator, IUserRoleRepository roleRepository)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userrepository;
        _tokenService = token;
        _userValidator = userValidator;
        _userLoginValidator = userLoginValidator;
        _roleRepository = roleRepository;

    }
    public async Task<long> SignUpAsync(UserCreateDto userCreateDto)
    {
        var validationResult = await _userValidator.ValidateAsync(userCreateDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var tupleFromHasher = PasswordHasher.Hasher(userCreateDto.Password);
        var user = new User()
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            UserName = userCreateDto.UserName,
            Email = userCreateDto.Email,
            PhoneNumber = userCreateDto.PhoneNumber,
            Password = tupleFromHasher.Hash,
            Salt = tupleFromHasher.Salt,
        };
        user.RoleId = await _roleRepository.GetRoleIdAsync("User");


        return await _userRepository.InsertAsync(user);



    }
    public async Task<LoginResponseDto> LogInAsync(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.SelectByUserNameAsync(userLoginDto.UserName);
        var checkpassword = PasswordHasher.Verify(userLoginDto.Password, user.Password, user.Salt);
        if (user is null || !checkpassword)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }
        var validationResult = await _userLoginValidator.ValidateAsync(userLoginDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var userDto=MapUserService.ConvertToDto(user);
        var accessToken = _tokenService.GenerateToken(userDto);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenToDB = new RefreshToken()
        {
            Token = refreshToken,
            Expire = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };
        await _refreshTokenRepository.InsertRefreshTokenAsync(refreshTokenToDB);

        return new LoginResponseDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            TokenType= "Bearer",
            Expires=24,
        };
       
    }

    public Task LogOutAsync(string refreshToken)=> _refreshTokenRepository.RemoveRefreshTokenAsync(refreshToken);

    public async Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto request)
    {
        ClaimsPrincipal? principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null) throw new ForbiddenException("Invalid access token.");


        var userClaim = principal.FindFirst(c => c.Type == "UserId");
        var userId = long.Parse(userClaim.Value);


        var refreshToken = await _refreshTokenRepository.SelectRefreshTokenAsync(request.RefreshToken, userId);
        if (refreshToken == null || refreshToken.Expire < DateTime.UtcNow || refreshToken.IsRevoked)
            throw new UnauthorizedException("Invalid or expired refresh token.");

        refreshToken.IsRevoked = true;

        var user = await _userRepository.SelectByIdAsync(userId);

        var userGetDto = MapUserService.ConvertToDto(user);


        var newAccessToken = _tokenService.GenerateToken(userGetDto);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenToDB = new RefreshToken()
        {
            Token = newRefreshToken,
            Expire = DateTime.UtcNow.AddDays(21),
            IsRevoked = false,
            UserId = user.UserId
        };

        await _refreshTokenRepository.InsertRefreshTokenAsync(refreshTokenToDB);

        return new LoginResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            TokenType = "Bearer",
            Expires = 24
        };
    }


}
