using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystemCycling.Data;
using SchoolSystemCycling.Models.Entities;
using SchoolSystemCycling.Services;

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

Console.WriteLine("\n=========== GetInstructorByIdWithDept() =============\n");

Instructor? instructorWithDept = await _basicQueryService.GetInstructorByIdWithDept(3);

/*
    Which of the below Console Writes causes an exception to be thrown? What is the exception?
*/

// Console.WriteLine($"1) {instructorWithDept}");

// Console.WriteLine($"2) Instructor {instructorWithDept.LastName} in Dept: {instructorWithDept.Department.Name}");

// Console.WriteLine($"3) Dept: {instructorWithDept.Department}");

// Console.WriteLine($"4) Instructor Object Serialized: \n{JsonSerializer.Serialize(instructorWithDept)}");

Console.WriteLine("\n=========== GetAllCoursesWithInstructorAndDepartment - Entities & DTOs =============\n");
// var courses = _basicQueryService.GetAllCoursesWithInstructorAndDepartmentAsync(); // Method that returns entities.
var courses = _basicQueryService.GetAllCourseDtosWithInstructorAndDepartmentAsync(); // Method that returns DTOs.

Console.WriteLine($"Courses with Instructors & Dept:\n\n{JsonSerializer.Serialize(courses, jsonOptions)}");

Console.WriteLine("\n=========== GetStudentCountsPerCourse - DTOs =============\n");

var info = await _basicQueryService.GetStudentCountsPerCourseAsync();
Console.WriteLine($"info object:\n{JsonSerializer.Serialize(info, jsonOptions)}");

Console.WriteLine("\n=========== GetStudentCourseCountsAsync - List of DTOs =============\n");

var infoList = await _basicQueryService.GetStudentCourseCountsAsync();
Console.WriteLine($"info list object:\n{JsonSerializer.Serialize(infoList, jsonOptions)}");

