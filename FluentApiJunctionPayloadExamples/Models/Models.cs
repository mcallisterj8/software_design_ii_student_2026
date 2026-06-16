namespace FluentApiJunctionPayloadExamples.Models;

public class Post {
    public int Id { get; set; }
    public List<Tag> Tags { get; } = [];
}

public class Tag {
    public int Id { get; set; }
    public List<Post> Posts { get; } = [];
}

public class PostTag {
    public int Pid { get; set; }
    public int Tid { get; set; }
    /* 
	    "Payload" properties. The one or more properties in junction 
	    entity which are in addition to the primary foreign key 
		  properties.
    */
    public string Notes { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}