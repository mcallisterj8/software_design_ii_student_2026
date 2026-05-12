using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolSystem.Models;

public class Course {
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    [ForeignKey("Instructor")]
    public int InstructorId { get; set; }
    public virtual Instructor? Instructor { get; set; }
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    public virtual Department? Department { get; set; }
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}