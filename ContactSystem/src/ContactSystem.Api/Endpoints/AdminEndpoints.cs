using ContactSystem.Application.DTOs;
using ContactSystem.Application.Services.UserService;
using ContactSystem.Core.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ContactSystem.Api.Endpoints;

public static class AdminEndpoints
{
    public static void MapAdminEndpoints(this WebApplication app)
    {
        var mapGroup = app.MapGroup("/Admin")
            .RequireAuthorization()
            .WithTags("AdminManagement");

        mapGroup.MapGet("/get all users", [Authorize(Roles="Admin,SuperAdmin")]
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, NoStore = false)]
        async (int skip , int take,IUserService userService) =>
            {
                var users = await userService.GetAllAsync(skip,take);
                return Results.Ok(users);
            })
            .WithName("GetAllUsers")
            .Produces(200)
            .Produces(400);

        mapGroup.MapGet("/get user by id/{userId}", [Authorize(Roles = "Admin,SuperAdmin")]
        async (long userId, IUserService userService) =>
            {
                var user = await userService.GetByIdAsync(userId);
                if (user == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(user);
            })
            .WithName("GetUserById")
            .Produces<UserDto>(200)
            .Produces(404);

        mapGroup.MapDelete("/delete user/{userId}", [Authorize(Roles = "Admin,SuperAdmin")]
        async(long userId ,IUserService userService)=>
                    {
                      var user = await userService.GetByIdAsync(userId);
                        await userService.DeleteAsync(user.UserId, user.Role.RoleName);
                    })
            .WithName("DeleteUser")
            .Produces(200)
            .Produces(404)
            .Produces(403);

        mapGroup.MapPatch("/updateRole", [Authorize(Roles="SuperAdmin")]
        async (long userId,string userRole,IUserService userService)=>
        {
            await userService.UpdateUserRoleAsync(userId, userRole);

        })
            .WithName("UpdateUserRole")
            .Produces(200)
            .Produces(404)
            .Produces(403);

    }
}
