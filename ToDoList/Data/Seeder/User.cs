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
        }
    }


    private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdministratorRole);

        if (alreadyExists) return;

        await roleManager.CreateAsync(new IdentityRole(Constants.AdministratorRole));
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
}