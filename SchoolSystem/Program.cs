using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystem.Data;
using SchoolSystem.Services;

ServiceProvider _serviceProvider;
SeedingService _seedingService;

// Create container to hold services for dependency injection
var services = new ServiceCollection();

// Add services to service container
services.AddDbContext<ApplicationDbContext>();
services.AddScoped<SeedingService>();

/*
    Get the service provider - this is our way to take something
    out of the container.
*/
_serviceProvider = services.BuildServiceProvider();

// Retrieve instance of SeedingService from the container
_seedingService = _serviceProvider.GetRequiredService<SeedingService>();

// Call method to seed the database
await _seedingService.SeedDatabase();




