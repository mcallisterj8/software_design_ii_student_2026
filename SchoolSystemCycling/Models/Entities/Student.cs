using System.ComponentModel.DataAnnotations;

namespace SchoolSystemCycling.Models.Entities;

public class Student {
    [Key]
    public int Id { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public DateTime JoiningDate { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

}