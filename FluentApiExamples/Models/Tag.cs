using System.ComponentModel.DataAnnotations;

namespace FluentApiExamples.Models;

public class Tag {
    [Key]
    public int Id { get; set; }
    public List<Post> Posts { get; } = [];
}

