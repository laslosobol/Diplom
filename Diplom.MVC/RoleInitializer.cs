using Microsoft.AspNetCore.Identity;

namespace Diplom.MVC;

public static class RoleInitializer
{
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
        await roleManager.CreateAsync(new IdentityRole("Customer"));
        await roleManager.CreateAsync(new IdentityRole("Courier"));
    }
}