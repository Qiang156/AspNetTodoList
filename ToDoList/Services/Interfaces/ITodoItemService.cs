using System;

using ToDoList.Models.Entity;
using ToDoList.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Services.Interface
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> getIncompleteItemsAsync(
            ApplicationUser user
        );

        Task<bool> addItemAsync(TodoItem Item, ApplicationUser user);

        Task<bool> MarkDoneAsync(Guid Id, ApplicationUser user);
        Task<bool> SoftDeleteAsync(Guid Id, ApplicationUser user);
    }
}
