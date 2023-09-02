using Microsoft.AspNetCore.Identity;

namespace Api.UserDtos;

public class RoleInitializer
{
    public static async Task InitializeRoles(RoleManager<IdentityRole> roleManager)
    {
        // Define your roles here
        string[] roleNames = { "Admin", "Manager", "User" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
