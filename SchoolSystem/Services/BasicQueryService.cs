
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
        return await _context.Instructors.FindAsync(instructorId);
    }

    public async Task<List<Department>> GetDepartmentsWithMoreThanOneCourseAsync() {
        return await _context.Departments
            .Where(dept => dept.Courses.Count > 0)
            .ToListAsync();
    }

}