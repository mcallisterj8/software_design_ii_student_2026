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

    public async Task<List<string>> GetAllInstructorNames() {
        return await _context.Instructors
            .Select(instructor => instructor.LastName)
            .ToListAsync();
    }

    public async Task<Instructor?> GetInstructorById(int instructorId) {
        // return await _context.Instructors.FindAsync(instructorId); // This is EF Core Find() - different than regular LINQ Find()

        return await _context.Instructors
            .Include(instr => instr.Department)
            .SingleOrDefaultAsync(instr => instr.Id == instructorId);
    }

    public async Task<InstructorDto?> GetInstructorDtoById(int instructorId) {
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


    public List<Student> GetStudentsInCourse(string courseName) {
        return _context.Courses.Where(course => course.Name == courseName)
                .SelectMany(course => course.Students)
                // .Select(student => student.LastName)
                .ToList();
    }

    public List<Department> GetDepartmentsWithMoreThanOneCourses() {
        return _context.Departments
                .Where(department => department.Courses.Count > 0)
                .ToList();
    }

    public string GetDepartmentWithMostCourses() {
        return _context.Departments
                .OrderByDescending(department => department.Courses.Count)
                .Select(department => department.Name)
                .FirstOrDefault();
    }

    public List<Student> GetStudentsEnrolledInMoreThanFiveCourses() {
        return _context.Students
                .Where(student => student.Courses.Count > 5)
                .ToList();
    }

    /*
        Talk about the SelectMany() and how this flattens a list.
    */
    public async Task<List<Instructor>> GetInstructorsInDepartment(string departmentName) {
        return await _context.Departments
                .Where(department => department.Name == departmentName)
                .SelectMany(department => department.Courses
                    .Select(course => course.Instructor))
                .Distinct()
                .ToListAsync();
    }

    public List<Course> GetCoursesByInstructor(string instructorName) {
        return _context.Instructors
            .Where(instructor => instructor.LastName == instructorName)
            .SelectMany(instructor => instructor.Courses)
            // .Select(course => course.Name))
            .ToList();
    }

    public List<Student> GetStudentsWithNoCourses() {
        return _context.Students
                .Where(student => !student.Courses.Any())
                // .Select(student => student.LastName)
                .ToList();
    }

    public Instructor GetInstructorWithMostCourses() {
        return _context.Instructors
            .OrderByDescending(instructor => instructor.Courses.Count)
            // .Select(instructor => instructor.LastName)
            .FirstOrDefault();
    }

    /*
        This method groups students by the year they enrolled. 
        After grouping, it then converts the grouped data into a Dictionary. 
        The keys of the dictionary are enrollment years (group.Key), 
        and the values are lists of student names who enrolled in the 
        respective year (group.Select(s => s.Name).ToList()).
    */
    public Dictionary<int, List<string>> GetStudentsGroupedByEnrollmentYear() {
        return _context.Students
            .GroupBy(s => s.JoiningDate.Year)  // extract the Year from JoiningDate
            .ToDictionary(group => group.Key, group => group.Select(s => s.LastName).ToList());
    }


    /*
        This method groups courses by the department name. After grouping, it 
        then converts the grouped data into a Dictionary. The keys of the 
        dictionary are department names (group.Key), and the values are lists of 
        course names that belong to the respective department 
        (group.Select(c => c.CourseName).ToList()).
    */
    public async Task<Dictionary<string, List<string>>> GetCoursesGroupedByDepartment() {
        return await _context.Courses
            .GroupBy(c => c.Department.Name)
            .ToDictionaryAsync(group => group.Key, group => group.Select(c => c.Name).ToList());
    }

    /*
        In this method, we first use SelectMany to flatten the collection of students along with their 
        associated courses into a single collection. Here, we're using an overload of SelectMany that 
        projects each student and course into a new anonymous object.

        Then we group these objects by the course name (sc.course.CourseName), and finally, we construct a 
        dictionary where each key is a course name and each value is a list of last names of the students enrolled in that course. 
        Note that sc is a shorthand for the anonymous object with properties student and course.

    */
    public Dictionary<string, List<string>> GetStudentsGroupedByCourse() {
        return _context.Students
            .SelectMany(s => s.Courses, (student, course) => new { student, course })
            .GroupBy(sc => sc.course.Name)
            .ToDictionary(group => group.Key, group =>
                group
                .Select(sc => sc.student.LastName)
                .ToList()
            );


    }
    public async Task<List<List<Course>>> GetStudentCourses() {
        return await _context.Students
            .Include(s => s.Courses)
            .Select(s => s.Courses.ToList()) // Needing to do the ToList() b/c s.Courses is an ICollection<T>
            .ToListAsync();
    }

    public async Task<List<Course>> GetStudentCoursesFlattened() {
        return await _context.Students
            .Include(s => s.Courses)
            .SelectMany(s => s.Courses) // Needing to do the ToList() b/c s.Courses is an ICollection<T>
            .ToListAsync();
    }

}