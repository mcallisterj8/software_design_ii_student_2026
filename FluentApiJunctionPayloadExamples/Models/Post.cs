namespace FluentApiJunctionPayloadExamples.Models;

public class Post {
    public int Id { get; set; }
    public List<Tag> Tags { get; } = [];
}