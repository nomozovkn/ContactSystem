using ContactSystem.Application.Services.UserRoleService;
using Microsoft.AspNetCore.Authorization;

namespace ContactSystem.Api.Endpoints;

public static class RoleEndpoints
{
    public static void MapRoleEndpoints(this WebApplication app)
    {
        var roleGroup = app.MapGroup("/roles")
            .RequireAuthorization()
            .WithTags("Roles");

        roleGroup.MapGet("/get all roles",
            async(IUserRoleService roleService) =>
            {
                var roles = await roleService.GetAllRolesAsync();
                return Results.Ok(roles);
            })
            .WithName("GetAllRoles")
            .Produces(200)
            .Produces(400);

        roleGroup.MapGet("/get all users by role", [Authorize(Roles="Admin,SuperAdmin")]
            async(string role, IUserRoleService roleService) =>
            {
                var users = await roleService.GetAllUsersByRoleAsync(role);
                return Results.Ok(users);
            })
            .WithName("GetAllUsersByRole")
            .Produces(200)
            .Produces(400);



    }
}
