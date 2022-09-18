using System;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Models.Entity;
using ToDoList.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;


namespace ToDoList.unitTests;

public class TestService : IDisposable
{

    protected readonly ApplicationDbContext _context;

    public TestService()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task TestAddItemAsync()
    {
        var service = new TodoItemService(_context);
        var fakeUser = new ApplicationUser
        {
            Id = "fake-000",
            UserName = "fake@example.com",
            Email = "fake@example.com"
        };
        
        await service.addItemAsync(new TodoItem { title = "Testing?" }, fakeUser);

        
        Assert.Equal(1, await _context.Item.CountAsync());
        
        var item = await _context.Item.FirstAsync();
        Assert.Equal("fake-000", item.userId);
        Assert.Equal("Testing?", item.title);
        Assert.False(item.isDone);
        // Assert.True(DateTimeOffset.Now.AddDays(1) - item.dueAt < TimeSpan.FromSeconds(1));

    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}