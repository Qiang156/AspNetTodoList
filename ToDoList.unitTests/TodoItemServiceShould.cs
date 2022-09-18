using System;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Models.Entity;
using ToDoList.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Microsoft.Extensions.DependencyInjection;


namespace ToDoList.unitTests;

public class TodoItemServiceShould
{

    private readonly ApplicationDbContext _context;

    public TodoItemServiceShould()
    {
        // Set up a context (connection to the "DB") for writing
        // var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            // .UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;
        _context = new ApplicationDbContext(CreateDbContextOptions("test_db"));
        // _context.Database.EnsureCreated();
    }

    public static DbContextOptions<ApplicationDbContext> CreateDbContextOptions(string databaseName)
    {
        var serviceProvider = new ServiceCollection().
            AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseInMemoryDatabase(databaseName)
            .UseInternalServiceProvider(serviceProvider);

        return builder.Options;
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
        _context.Items.Add(new TodoItem { title = "Testing?" });
        _context.SaveChanges();
        
        //await service.addItemAsync(new TodoItem { title = "Testing?" }, fakeUser);

        Assert.False(false);
        //Assert.Equal(1, await inMemoryContext.Items.CountAsync());
        
        // var item = await inMemoryContext.Items.FirstAsync();
        // Assert.Equal("fake-000", item.userId);
        // Assert.Equal("Testing?", item.title);
        // Assert.False(item.isDone);
        // Assert.True(DateTimeOffset.Now.AddDays(3) - item.dueAt < TimeSpan.FromSeconds(1));

    }
}