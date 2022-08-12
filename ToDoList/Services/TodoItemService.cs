using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ToDoList.Data;
using ToDoList.Models.Entity;
using ToDoList.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Services.Interface
{
    
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _context;

        public TodoItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem[]> getIncompleteItemsAsync(ApplicationUser user)
        {
            
            return await _context.Items.Where(
                item => item.isDelete == false && item.userId == user.Id
                ).ToArrayAsync();

        }

        public async Task<bool> addItemAsync(TodoItem Item, ApplicationUser user)
        {
            Item.id = Guid.NewGuid();
            Item.isDone = false;
            Item.dueAt = DateTimeOffset.Now.AddDays(-1);
            Item.createdAt = DateTimeOffset.Now.DateTime;
            Item.userId = user.Id;

            _context.Items.Add(Item);
            
            var res = await _context.SaveChangesAsync();

            return res == 1;
        }
        public async Task<bool> MarkDoneAsync(Guid id, ApplicationUser user)
        {
            var item = await _context.Items.Where(
                Item => Item.id == id && Item.userId == user.Id
                ).SingleOrDefaultAsync();                
            if (item == null) return false;

            item.isDone = true;
            item.updatedAt = DateTimeOffset.Now.DateTime;
            var res = await _context.SaveChangesAsync();

            return res == 1;
        }

        public async Task<bool> SoftDeleteAsync(Guid id, ApplicationUser user)
        {
            var item = await _context.Items.Where(
                Item => Item.id == id && Item.userId == user.Id
                ).SingleOrDefaultAsync();
            if (item == null) return false;

            item.isDelete = true;
            var res = await _context.SaveChangesAsync();
            
            return res == 1;
        }

    }
}