using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Models.Entity;
using ToDoList.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {

        private readonly ITodoItemService _todoItemService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TodoController( ITodoItemService todoItemService, UserManager<ApplicationUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }
        
        // Todo list Homepage
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var items = await _todoItemService.getIncompleteItemsAsync(currentUser);
            var model = new TodoViewModel()
            {
                Items = items
            };
            return View(model);
        }

        // Add Todo list record
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem Item)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            if (!ModelState.IsValid) {
                //return RedirectToAction("Index");
            }

            var successful = await _todoItemService.addItemAsync(Item, currentUser);
            if(!successful) {
                return BadRequest("Could not add item.");
            }

            return RedirectToAction("Index");

        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(String action, Guid id)
        {
            if (id == Guid.Empty) {
                return RedirectToAction("Index");
            }
            if (action != "MarkDone" && action != "Delete") {
                return RedirectToAction("Index");
            }
            
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var message = String.Empty;
            var successful = false;

            if ( action == "MarkDone" ) {
                successful = await _todoItemService.MarkDoneAsync(id, currentUser);
                message = "Could not mark item as done.";
            } else {
                successful = await _todoItemService.SoftDeleteAsync(id, currentUser);
                message = "Could not be deleted.";
            }
            if(!successful) {
                return BadRequest(message);
            }

            return RedirectToAction("Index");

        }
    }
}