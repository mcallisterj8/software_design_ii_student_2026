
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.Data;
using SchoolSystem.Models;

namespace SchoolSystem.Services;

public class BasicQueryService {
    // An attribute for the ApplicationDbContext
    private readonly ApplicationDbContext _context;

    public BasicQueryService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<List<string>> GetAllInstructorNamesAsync() {
        return await _context.Instructors
            .Select(instructor => instructor.LastName)
            .ToListAsync();
    }

    public async Task<Instructor?> GetInstructorByIdAsync(int instructorId) {
        // return await _context.Instructors.FindAsync(instructorId);
        return await _context.Instructors
            .Include(instr => instr.Department)
            .Include(instr => instr.Courses)
            .SingleOrDefaultAsync(instr => instr.Id == instructorId);
    }

    public async Task<List<Department>> GetDepartmentsWithMoreThanOneCourseAsync() {
        return await _context.Departments
            .Where(dept => dept.Courses.Count > 0)
            .ToListAsync();
    }

    public async Task<string?> GetDepartmentWithMostCoursesAsync() {
        return await _context.Departments
            .OrderByDescending(dept => dept.Courses.Count)
            .Select(dept => dept.Name)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Student>> GetStudentsEnrolledInMoreThanFiveCoursesAsync() {
        return await _context.Students
            .Where(student => student.Courses.Count > 5)
            .ToListAsync();
    }

    public async Task<List<Student>> GetStudentsWithNoCoursesAsync() {
        return await _context.Students
            // .Where(student => student.Courses.Count == 0)
            .Where(student => !student.Courses.Any())
            .ToListAsync();
    }

    public async Task<Instructor?> GetInstructorWithMostCoursesAsync() {
        return await _context.Instructors
            .OrderByDescending(instr => instr.Courses.Count)
            .FirstOrDefaultAsync();
    }

    public async Task<List<List<Course>>> GetAllStudentCoursesAsync() {
        return await _context.Students
            .Include(stud => stud.Courses)
            .Select(stud => stud.Courses.ToList())
            .ToListAsync();
    }

    public async Task<List<Course>> GetAllStudentCoursesFlattenedAsync() {
        return await _context.Students
            .Include(stud => stud.Courses)
            .SelectMany(stud => stud.Courses)
            .ToListAsync();
    }

}