using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystem.Data;
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

// Call method to seed the database.
await _seedingService.SeedDatabase();

JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };

var coursesByDept = await _basicQueryService.GetCoursesGroupedByDepartment();

Console.WriteLine($"\n{JsonSerializer.Serialize(coursesByDept, jsonOptions)}\n");


Console.WriteLine("\n=========== GetStudentCourses() =============\n");

var studentCourses = await _basicQueryService.GetStudentCourses();

Console.WriteLine($"\n{JsonSerializer.Serialize(studentCourses, jsonOptions)}\n");


Console.WriteLine("\n=========== GetStudentCoursesFlattened() =============\n");

var studentCoursesFlattened = await _basicQueryService.GetStudentCoursesFlattened();

Console.WriteLine($"\n{JsonSerializer.Serialize(studentCoursesFlattened, jsonOptions)}\n");


