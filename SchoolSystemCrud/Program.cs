using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using SchoolSystemCrud.Data;
using SchoolSystemCrud.Models;
using SchoolSystemCrud.Services;

ServiceProvider _serviceProvider;
SeedingService _seedingService;
CrudService _crudService;

// Create container to hold services for dependency injection
var services = new ServiceCollection();

// Add services to service container
services.AddDbContext<ApplicationDbContext>();
services.AddScoped<SeedingService>();
services.AddScoped<CrudService>();

/*
    Get the service provider - this is our way to take something
    out of the container.
*/
_serviceProvider = services.BuildServiceProvider();

// Retrieve instance of the SeedingService from the container
_seedingService = _serviceProvider.GetRequiredService<SeedingService>();
// Retrieve instance of the CrudService from the container
_crudService = _serviceProvider.GetRequiredService<CrudService>();

// Seeding the database
await _seedingService.SeedDatabase();

Console.WriteLine("========== SCHOOL CRUD TESTS ==========\n");

// 1.Create a new student
Console.WriteLine("=> Creating student...");
var newStudent = new Student
{
    FirstName = "Jane",
    LastName = "Doe",
    JoiningDate = new DateTime(2025, 5, 20)
};
await _crudService.AddStudentAsync(newStudent);

var insertedStudent = await _crudService.GetStudentByNameAsync("Jane", "Doe");
Console.WriteLine(insertedStudent != null
    ? $"Student added with ID: {insertedStudent.Id}"
    : "Failed to insert student");

// 2. Update the student's name
Console.WriteLine("\n=> Updating student's name...");
if (insertedStudent != null)
{
    await _crudService.UpdateStudentNameAsync(insertedStudent.Id, "Janet", "Smith");

    var updatedStudent = await _crudService.GetStudentByIdAsync(insertedStudent.Id);
    Console.WriteLine(updatedStudent?.FirstName == "Janet"
        ? $"Updated student name to: {updatedStudent.FirstName} {updatedStudent.LastName}"
        : "Failed to update student name");
}

// 3. Enroll student in course ID 1
Console.WriteLine("\n=> Enrolling student in Course ID 1...");
if (insertedStudent != null)
{
    var enrollmentResult = await _crudService.EnrollStudentInCourseAsync(insertedStudent.Id, 1);
    var enrolledStudents = await _crudService.GetStudentsInCourseAsync(1);

    Console.WriteLine(enrollmentResult && enrolledStudents.Any(s => s.Id == insertedStudent.Id)
        ? $"Student enrolled in course. Total students in course: {enrolledStudents.Count}"
        : "Failed to enroll student");
}

// 4. Delete the student
Console.WriteLine("\n=> Deleting student...");
if (insertedStudent != null)
{
    var deleteSuccess = await _crudService.DeleteStudent(insertedStudent.Id);
    var checkStudent = await _crudService.GetStudentByIdAsync(insertedStudent.Id);

    Console.WriteLine(deleteSuccess && checkStudent == null
        ? "Student successfully deleted."
        : "Failed to delete student.");
}

Console.WriteLine("\n========== TESTS COMPLETE ==========");
