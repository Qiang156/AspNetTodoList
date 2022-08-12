using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Models.Entity;
using ToDoList.Models;

namespace ToDoList.Services.Interface
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<TodoItem[]> getIncompleteItemsAsync(ApplicationUser user)
        {
            var Items = new[] {
                new TodoItem{
                    title = "Dependency injection in ASP.NET Core",
                    dueAt = DateTimeOffset.Now.AddDays(1),
                    createdAt = DateTimeOffset.Now.AddDays(-1),
                    userId = user.Id
                },
                new TodoItem{
                    title = "ASP.NET Core documentation - what's new?",
                    dueAt = DateTimeOffset.Now.AddDays(2),
                    createdAt = DateTimeOffset.Now.AddDays(-2),
                    userId = user.Id
                },
                new TodoItem{
                    title = "ASP.NET Core documentation - what's new?",
                    dueAt = DateTimeOffset.Now.AddDays(3),
                    createdAt = DateTimeOffset.Now.AddDays(-3),
                    userId = user.Id
                },
                new TodoItem{
                    title = "ASP.NET Core documentation - what's new?",
                    dueAt = DateTimeOffset.Now.AddDays(4),
                    createdAt = DateTimeOffset.Now.AddDays(-4),
                    userId = user.Id
                },
                new TodoItem{
                    title = "ASP.NET Core documentation - what's new?",
                    dueAt = DateTimeOffset.Now.AddDays(5),
                    createdAt = DateTimeOffset.Now.AddDays(-5),
                    userId = user.Id
                }
            };
            return Task.FromResult(Items);
        }

        public Task<bool> addItemAsync(TodoItem Item, ApplicationUser user)
        {
            return Task.FromResult(true);
        }

        public Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            return Task.FromResult(true);
        }

        public Task<bool> SoftDeleteAsync(Guid id, ApplicationUser user)
        {
            return Task.FromResult(true);
        }
    }
}
