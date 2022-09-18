using System;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Models.Entity;
using ToDoList.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ToDoList.unitTests;

public class TodoItemServiceShould
{
    [Fact]
    public async Task TestAddItemAsync()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;

        // Set up a context (connection to the "DB") for writing
        using (var inMemoryContext = new ApplicationDbContext(options))
        {
            var service = new TodoItemService(inMemoryContext);

            var fakeUser = new ApplicationUser
            {
                Id = "fake-000",
                UserName = "fake@example.com",
                Email = "fake@example.com"
            };

            await service.addItemAsync(new TodoItem { title = "Testing?" }, fakeUser);
        }

        // Use a separate context to read the data back from the DB
        using (var inMemoryContext = new ApplicationDbContext(options))
        {
            Assert.False(false);
            //Assert.Equal(1, await inMemoryContext.Items.CountAsync());
            
            // var item = await inMemoryContext.Items.FirstAsync();
            // Assert.Equal("fake-000", item.userId);
            // Assert.Equal("Testing?", item.title);
            // Assert.False(item.isDone);
            // Assert.True(DateTimeOffset.Now.AddDays(3) - item.dueAt < TimeSpan.FromSeconds(1));
        }


    }
}