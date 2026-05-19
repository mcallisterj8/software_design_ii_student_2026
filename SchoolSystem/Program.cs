using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystem.Data;
using SchoolSystem.Models;
using SchoolSystem.Services;

ServiceProvider _serviceProvider;
SeedingService _seedingService;
BasicQueryService _basicQueryService;

// Create container to hold services for dependency injection
var services = new ServiceCollection();

// Add services to service container
services.AddDbContext<ApplicationDbContext>();
services.AddScoped<SeedingService>();
services.AddScoped<BasicQueryService>();

/*
    Get the service provider - this is our way to take something
    out of the container.
*/
_serviceProvider = services.BuildServiceProvider();

// Retrieve instance of SeedingService from the container
_seedingService = _serviceProvider.GetRequiredService<SeedingService>();

// Retrieve instance of BasicQueryService from the container
_basicQueryService = _serviceProvider.GetRequiredService<BasicQueryService>();

// Call method to seed the database
await _seedingService.SeedDatabase();

// Get all instructor names
List<string> instructorNames = await _basicQueryService.GetAllInstructorNamesAsync();

foreach (string name in instructorNames) {
    Console.WriteLine(name);
}

Console.WriteLine("======================== GetInstructorByIdAsync ===================");
Instructor? person = await _basicQueryService.GetInstructorByIdAsync(1);
Console.WriteLine($"{person.FirstName} {person.LastName} works in {person.Department.Name}");

Console.WriteLine("======================== Depts with more than one course ===================");
List<Department> depts = await _basicQueryService.GetDepartmentsWithMoreThanOneCourseAsync();

foreach (var dept in depts) {
    Console.WriteLine(dept.Name);
}





