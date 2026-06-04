using SchoolSystemCrud.Models;
using SchoolSystemCrud.Data;
using Microsoft.EntityFrameworkCore;

namespace SchoolSystemCrud.Services;

public class CrudService {
    private readonly ApplicationDbContext _context;

    public CrudService(ApplicationDbContext context) {
        _context = context;
    }

    // Example: Get all students
    public async Task<List<Student>> GetAllStudentsAsync() {
        return await _context.Students.ToListAsync();
    }

    public async Task<List<Student>> GetAllStudentsWithCoursesAsync() {
        // Using Include() to load related Courses data
        return await _context.Students
            .Include(s => s.Courses)
            .ToListAsync();
    }

    public async Task<List<Course>> GetAllCoursesWithStudentsAsync() {
        // Using Include() to load related Students data
        return await _context.Courses
        .Include(c => c.Students)
        .ToListAsync();
    }

    // Example: Get student by ID, with their courses
    public async Task<Student?> GetStudentByIdAsync(int id) {
        return await _context.Students
            .Include(s => s.Courses)
            .SingleOrDefaultAsync(s => s.Id == id);
    }

    // Get a student by full name (for testing inserts and updates)
    public async Task<Student?> GetStudentByNameAsync(string firstName, string lastName) {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.FirstName == firstName && s.LastName == lastName);
    }

    // Get all students in a course
    public async Task<List<Student>> GetStudentsInCourseAsync(int courseId) {
        var course = await _context.Courses
            .Include(c => c.Students)
            .FirstOrDefaultAsync(c => c.Id == courseId);

        return course?.Students.ToList() ?? new List<Student>();
    }


    public async Task<Student?> GetStudentByIdTestAsync(int id) {
        return await _context.Students
            //    .Include(s => s.Courses)
            .SingleOrDefaultAsync(s => s.Id == id);
    }

    // Example: Add a new student
    public async Task<bool> AddStudentAsync(Student student) {
        if (null == student) {
            return false;
        }

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return true;
    }

    // Example: Update a student
    public async Task<Student?> UpdateStudentAsync(Student studentDetails) {
        var student = await _context.Students.FindAsync(studentDetails.Id);

        if (null != student) {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }
        return student;
    }

    public async Task<Student?> UpdateStudentNameAsync(int studentId, string newFirstName, string newLastName) {
        // Find the student, update the name, and save changes
        var student = await _context.Students.FindAsync(studentId);

        if (null != student) {
            student.FirstName = newFirstName;
            student.LastName = newLastName;

            await _context.SaveChangesAsync();
        }

        return student;
    }

    public async Task<bool> EnrollStudentInCourseAsync(int studentId, int courseId) {
        // Fetch the student and course, then update the models and save changes
        var student = await _context.Students.FindAsync(studentId);

        if (null == student) {
            return false;
        }

        var course = await _context.Courses
            .Include(c => c.Students)
            .SingleOrDefaultAsync(c => c.Id == courseId);

        if (null == course) {
            return false;
        }

        course.Students.Add(student);
        await _context.SaveChangesAsync();

        return true;
    }


    // Example: Delete a student
    public async Task<bool> DeleteStudent(int id) {
        bool result = false;
        var student = await _context.Students.FindAsync(id);

        if (null != student) {
            _context.Students.Remove(student);

            await _context.SaveChangesAsync();
            result = true;
        }

        return result;
    }

}
