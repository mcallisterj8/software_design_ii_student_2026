using SchoolSystem.Models;
using SchoolSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SchoolSystem.Models.Dtos;

namespace SchoolSystem.Services;

public class BasicQueryService {
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
        // return await _context.Instructors.FindAsync(instructorId); // This is EF Core Find() - different than regular LINQ Find()

        return await _context.Instructors
            .Include(instr => instr.Department)
            .SingleOrDefaultAsync(instr => instr.Id == instructorId);
    }

    public async Task<InstructorDto?> GetInstructorDtoByIdAsync(int instructorId) {
        /*
            Notice we have to refactor code a bit from the GetinstructorById() method.
            Single() and First() only take a predicate, then cannot do projection.
            So we move the predicate to a Where() and then do the Select() for the
            projection, and then do the Single() or First() method.
        */
        return await _context.Instructors
            .Where(instr => instr.Id == instructorId)
            .Include(instr => instr.Department)
            .Select(instr => new InstructorDto {
                LastName = instr.LastName,
                DepartmentName = instr.Department.Name
            })
            .SingleOrDefaultAsync();
    }


    public async Task<List<Student>> GetStudentsInCourseAsync(string courseName) {
        return await _context.Courses.Where(course => course.Name == courseName)
            .SelectMany(course => course.Students)
            // .Select(student => student.LastName)
            .ToListAsync();
    }

    public async Task<List<Department>> GetDepartmentsWithMoreThanOneCourseAsync() {
        return await _context.Departments
            .Where(department => department.Courses.Count > 0)
            .ToListAsync();
    }

    public async Task<string?> GetDepartmentWithMostCoursesAsync() {
        return await _context.Departments
            .OrderByDescending(department => department.Courses.Count)
            .Select(department => department.Name)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Student>> GetStudentsEnrolledInMoreThanFiveCoursesAsync() {
        return await _context.Students
            .Where(student => student.Courses.Count > 5)
            .ToListAsync();
    }

    /*
        Talk about the SelectMany() and how this flattens a list.
    */
    public async Task<List<Instructor?>> GetInstructorsInDepartmentAsync(string departmentName) {
        return await _context.Departments
                .Where(department => department.Name == departmentName)
                .SelectMany(department =>
                    department.Courses.Select(course => course.Instructor))
                .Distinct()
                .ToListAsync();
    }

    public async Task<List<Course>> GetCoursesByInstructorAsync(string instructorName) {
        /*
            Because we only want one list of courses returned, we need to do
            SelectMany() to flatten the list. If we did Select(), we would get
            one list of courses for each instructor which matched our Where()
            predicate. In our example, we may only have one instructor per last
            name, but we could conceivably have more than one instructor with
            the same last name.
        */
        return await _context.Instructors
            .Where(instructor => instructor.LastName == instructorName)
            .SelectMany(instructor => instructor.Courses)
            // .Select(course => course.Name))
            .ToListAsync();
    }

    public async Task<List<Student>> GetStudentsWithNoCoursesAsync() {
        return await _context.Students
                .Where(student => !student.Courses.Any())
                // .Select(student => student.LastName) // change return type to Task<List<string>> for this
                .ToListAsync();
    }

    public async Task<Instructor?> GetInstructorWithMostCoursesAsync() {
        return await _context.Instructors
            .OrderByDescending(instructor => instructor.Courses.Count)
            // .Select(instructor => instructor.LastName)
            .FirstOrDefaultAsync();
    }


    public async Task<List<List<Course>>> GetAllStudentCoursesAsync() {
        return await _context.Students
            .Include(s => s.Courses)
            // .ThenInclude(c => c.Students) // Uncomment to show a cycling issue. Check Program.cs to see needed actions there for demo-ing this.
            .Select(s => s.Courses.ToList()) // Needing to do the ToList() b/c s.Courses is an ICollection<T>
            .ToListAsync();
    }

    /*
        Use this method as intro as to SelectMany()
    */
    public async Task<List<Course>> GetAllStudentCoursesFlattenedAsync() {
        return await _context.Students
            .Include(s => s.Courses)
            .SelectMany(s => s.Courses) // Needing to do the ToList() b/c s.Courses is an ICollection<T>
            .ToListAsync();
    }

}