using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public class Seed
{
    public static async Task SeedData(AgriLinkDbContext context, UserManager<User> userManager, IConfiguration configuration)
    {
        // Check if there are no users in the database
        if (!context.Users.Any())
        {
            // Read the admin user details from appsettings.json
            var adminUser = configuration.GetSection("AdminUser");

            var userName = adminUser["UserName"];
            var email = adminUser["Email"];
            var password = adminUser["Password"];


            // Create a new instance of the AppUser
            var user = new User
            {
                UserName = userName,
                Email = email
            };

            // Create the admin user with the UserManager
            var result = await userManager.CreateAsync(user, password);
        }
    }
}
