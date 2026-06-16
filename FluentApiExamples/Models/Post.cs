using System.ComponentModel.DataAnnotations;

namespace FluentApiExamples.Models;

public class Post {
    [Key]
    public int Id { get; set; }
    public List<Tag> Tags { get; } = [];
}