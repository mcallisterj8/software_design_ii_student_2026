using SchoolSystemCycling.Models.Entities;
using SchoolSystemCycling.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SchoolSystemCycling.Models.Dtos;

namespace SchoolSystemCycling.Services;

public class BasicQueryService {
    private readonly ApplicationDbContext _context;

    public BasicQueryService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<Instructor?> GetInstructorByIdWithDept(int instructorId) {
        // return await _context.Instructors.FindAsync(instructorId); // return await _context.Instructors.FindAsync(instructorId); // This is EF Core Find() - different than regular LINQ Find()

        return await _context.Instructors
            .Include(instr => instr.Department)
            .SingleOrDefaultAsync(instr => instr.Id == instructorId);
    }

    public async Task<InstructorDto?> GetInstructorDtoById(int instructorId) {
        // return await _context.Instructors.FindAsync(instructorId); // return await _context.Instructors.FindAsync(instructorId); // This is EF Core Find() - different than regular LINQ Find()

        return await _context.Instructors
            .Where(instr => instr.Id == instructorId)
            .Include(instr => instr.Department)
            .Select(instr => new InstructorDto {
                LastName = instr.LastName,
                DepartmentName = instr.Department.Name
            })
            .SingleOrDefaultAsync();
    }

    /*
        The following will cause a cycling runtime error when you call this method in the Program.cs
    */
    public async Task<List<Course>> GetAllCoursesWithInstructorAndDepartmentAsync() {
        return await _context.Courses
            .Include(course => course.Instructor)
            .Include(course => course.Department)
            .ToListAsync();
    }

    /*
        Use the CourseDto in order to avoid the cycling error at runtime that the above method has.
    */
    public async Task<List<CourseDto>> GetAllCourseDtosWithInstructorAndDepartmentAsync() {
        return await _context.Courses
            .Include(course => course.Instructor)
            .Include(course => course.Department)
            .Select(course => new CourseDto {
                CourseName = course.Name,
                InstructorName = $"{course.Instructor.FirstName} {course.Instructor.LastName}",
                DepartmentName = course.Department.Name
            })
            .ToListAsync();
    }

    public async Task<List<Student>> GetStudentsJoinedAfterDate(DateTime joinDate) {
        return await _context.Students
            .Where(student => student.JoiningDate > joinDate)
            .OrderBy(student => student.JoiningDate)
            .ToListAsync();
    }

    public async Task<List<Instructor>> GetInstructorsByDepartmentAsync(int departmentId) {
        return await _context.Instructors
            .Where(instr => instr.DepartmentId == departmentId)
            .OrderBy(instr => instr.LastName)
            .ToListAsync();
    }

    public async Task<List<InfoDto>> GetStudentCountsPerCourseAsync() {
        return await _context.Courses
            .Select(course => new InfoDto {
                Name = course.Name,
                Count = course.Students.Count
            })
            .OrderByDescending(dto => dto.Count)
            .ToListAsync();
    }


    public async Task<List<InfoDto>> GetStudentCourseCountsAsync() {
        return await _context.Students
            .Select(student => new InfoDto {
                Name = $"{student.FirstName} {student.LastName}",
                Count = student.Courses.Count
            })
            .OrderByDescending(student => student.Count)
            .ToListAsync();
    }


}