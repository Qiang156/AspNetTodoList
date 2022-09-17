using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using ToDoList.Models.Entity;


namespace ToDoList.Controllers;

[Authorize(Roles = Constants.AdministratorRole)]
public class UserController : Controller
{

    private readonly UserManager<ApplicationUser> _userManager;

    public UserController( UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<IActionResult> Index()
    {
        var admins = ( await _userManager.GetUsersInRoleAsync(Constants.AdministratorRole) ).ToArray();

        var everyone = await _userManager.Users.ToArrayAsync();

        var model = new UsersViewModel
        {
            Administrators = admins,
            Everyone = everyone
        };

        return View(model);
    }


    public IActionResult AddUser()
    {
        return View();

    }
}