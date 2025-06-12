using ContactSystem.Application.DTOs;
using ContactSystem.Application.Services.AuthService;
using ContactSystem.Application.Services.UserService;

namespace ContactSystem.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/auth")
            .WithTags("Auth");

        authGroup.MapPost("/signup",
            async (UserCreateDto userDto, IAuthService authService) =>
            {
                await authService.SignUpAsync(userDto);
                return Results.Ok();
            })
            .WithName("SignUpUser")
            .Produces(200)
            .Produces(400);

        authGroup.MapPost("/login",
            async (UserLoginDto userLoginDto, IAuthService authService) =>
            {
                var response = await authService.LogInAsync(userLoginDto);
                return Results.Ok(response);
            })
            .WithName("LoginUser")
            .Produces(200)
            .Produces(400);

        authGroup.MapPost("/refresh-token",
            async (RefreshRequestDto request, IAuthService authService) =>
            {
                var response = await authService.RefreshTokenAsync(request);
                return Results.Ok(response);
            })
            .WithName("RefreshToken")
            .Produces(200)
            .Produces(400);

        authGroup.MapDelete("/logout",
            async (string refreshToken, IAuthService authService) =>
            {
                await authService.LogOutAsync(refreshToken);
                return Results.Ok();
            })
            .WithName("LogoutUser")
            .Produces(200)
            .Produces(400);
    }
}
