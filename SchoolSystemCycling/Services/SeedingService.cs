using SchoolSystemCycling.Data;
using SchoolSystemCycling.Models.Entities;

namespace SchoolSystemCycling.Services;
public class SeedingService {
    private readonly ApplicationDbContext _context;

    public SeedingService(ApplicationDbContext context) {
        _context = context;
    }

    public void CreateInstructor(string firstName, string lastName, DateTime joiningDate, int deptId) {
        var instructor = new Instructor {
            FirstName = firstName,
            LastName = lastName,
            JoiningDate = joiningDate,
            DepartmentId = deptId
        };
        _context.Instructors.Add(instructor);
    }

    public void CreateStudent(string firstName, string lastName, DateTime joiningDate) {
        var student = new Student {
            FirstName = firstName,
            LastName = lastName,
            JoiningDate = joiningDate
        };
        _context.Students.Add(student);
    }

    public void CreateDepartment(string deptName) {
        var department = new Department {
            Name = deptName
        };
        _context.Departments.Add(department);
    }

    public void CreateCourse(string name, int instructorId, int departmentId) {
        var course = new Course {
            Name = name,
            InstructorId = instructorId,
            DepartmentId = departmentId
        };
        _context.Courses.Add(course);
    }

    public void AddStudentToCourse(int studentId, int courseId) {
        var student = _context.Students.Find(studentId);
        var course = _context.Courses.Find(courseId);
        if (student != null && course != null) {
            course.Students.Add(student);
        }
    }

    public async Task SeedDatabase() {
        if (_context.Instructors.Any() || _context.Students.Any() || _context.Departments.Any() || _context.Courses.Any()) {
            return; // Database has already been seeded
        }

        // Create Departments
        CreateDepartment("Computer Science");
        CreateDepartment("Mathematics");
        CreateDepartment("Physics");

        await _context.SaveChangesAsync();

        // Get Department IDs
        var csDeptId = _context.Departments.First(d => d.Name == "Computer Science").Id;
        var mathDeptId = _context.Departments.First(d => d.Name == "Mathematics").Id;
        var physicsDeptId = _context.Departments.First(d => d.Name == "Physics").Id;

        // Create Instructors
        CreateInstructor("John", "Doe", new DateTime(2010, 5, 1), physicsDeptId);
        CreateInstructor("Jane", "Smith", new DateTime(2012, 7, 23), mathDeptId);
        CreateInstructor("Alan", "Turing", new DateTime(2015, 3, 14), csDeptId);

        await _context.SaveChangesAsync();

        // Get Instructor IDs
        var instructorJohnId = _context.Instructors.First(i => i.LastName == "Doe").Id;
        var instructorJaneId = _context.Instructors.First(i => i.LastName == "Smith").Id;
        var instructorAlanId = _context.Instructors.First(i => i.LastName == "Turing").Id;

        // Create more courses with varying instructors and departments
        CreateCourse("Data Structures", instructorAlanId, csDeptId);
        CreateCourse("Computer Networks", instructorAlanId, csDeptId);
        CreateCourse("Operating Systems", instructorAlanId, csDeptId);

        CreateCourse("Linear Algebra", instructorJaneId, mathDeptId);
        CreateCourse("Statistics", instructorJaneId, mathDeptId);
        CreateCourse("Calculus", instructorJaneId, mathDeptId);

        CreateCourse("Particle Physics", instructorJohnId, physicsDeptId);
        CreateCourse("Quantum Mechanics", instructorJohnId, physicsDeptId);

        await _context.SaveChangesAsync();

        // Create more students
        CreateStudent("Eve", "Larson", new DateTime(2019, 9, 1));
        CreateStudent("Dave", "Lee", new DateTime(2019, 9, 1));
        CreateStudent("Gina", "Wong", new DateTime(2019, 9, 1));

        await _context.SaveChangesAsync();

        // Enroll students in various courses
        var allCourses = _context.Courses.ToList();
        var allStudents = _context.Students.ToList();

        // Randomly enroll students in 3 to 7 courses
        Random rand = new Random();
        foreach (var student in allStudents) {
            var shuffledCourses = allCourses.OrderBy(course => rand.Next()).Take(rand.Next(3, 8)).ToList();
            foreach (var course in shuffledCourses) {
                AddStudentToCourse(student.Id, course.Id);
            }
        }

        await _context.SaveChangesAsync();
    }
}

