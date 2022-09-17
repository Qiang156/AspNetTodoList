using System;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ToDoList.Data.Seeder;

public static class User
{
    public static async Task InitializeAsync(IApplicationBuilder app)
    {
        using(var serviceScope = app.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
            
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);

            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            await EnsureTestAdminAsync(userManager);
            await EnsureTestGuestAsync(userManager);
        }
    }


    private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);
        if (alreadyExists) return;
        await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));

        alreadyExists = await roleManager.RoleExistsAsync(Constants.GuestRole);
        if (alreadyExists) return;
        await roleManager.CreateAsync(new IdentityRole(Constants.GuestRole));
    }

    private static async Task EnsureTestAdminAsync(UserManager<ApplicationUser> userManager)
    {
        var testAdmin = await userManager.Users
            .Where(x => x.UserName == "admin@todo.local")
            .SingleOrDefaultAsync();

        if (testAdmin != null) return;

        testAdmin = new ApplicationUser 
        {
            UserName = "admin@todo.local",
            Email = "admin@todo.local"
        };
        await userManager.CreateAsync(testAdmin, "NotSecure123!!");
        await userManager.AddToRoleAsync(testAdmin, Constants.AdministratorRole);
        
    }

    private static async Task EnsureTestGuestAsync(UserManager<ApplicationUser> userManager)
    {
        var testGuest = await userManager.Users
            .Where(x => x.UserName == "guest@todo.local")
            .SingleOrDefaultAsync();

        if (testGuest != null) return;


        testGuest = new ApplicationUser 
        {
            UserName = "guest@todo.local",
            Email = "guest@todo.local"
        };
        await userManager.CreateAsync(testGuest, "NotSecure123!!");
        await userManager.AddToRoleAsync(testGuest, Constants.GuestRole);
        
    }

    // private static async Task ClearAllUserAsync(UserManager<ApplicationUser> userManager)
    // {
    //     // await userManager.Users.
    // }
}