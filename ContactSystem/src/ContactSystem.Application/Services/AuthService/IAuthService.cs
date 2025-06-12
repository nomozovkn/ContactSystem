using ContactSystem.Application.DTOs;

namespace ContactSystem.Application.Services.AuthService;

public interface IAuthService
{
    Task<long>SignUpAsync(UserCreateDto userCreateDto);
    Task<LoginResponseDto>LogInAsync(UserLoginDto userLoginDto);
    Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto request);
    Task LogOutAsync(string refreshToken);
}