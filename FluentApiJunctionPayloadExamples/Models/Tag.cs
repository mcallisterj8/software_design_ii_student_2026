namespace FluentApiJunctionPayloadExamples.Models;

public class Tag {
    public int Id { get; set; }
    public List<Post> Posts { get; } = [];
}