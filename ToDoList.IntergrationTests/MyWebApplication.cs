using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoList.Data;

namespace ToDoList.IntegrationTests;

public class MyWebApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public MyWebApplication(string environment = "Development")
    {
        _environment = environment;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment(_environment);
        builder.ConfigureServices(services => 
        {
            services.AddScoped(sp => 
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .UseApplicationServiceProvider(sp).Options;
                return options; 
            });
        });
        return base.CreateHost(builder);
    }
}