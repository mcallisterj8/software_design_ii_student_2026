using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.Models;

public class Department {
    [Key]
    public int Id { get; set; }

    public required string Name { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
}