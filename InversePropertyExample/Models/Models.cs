using System.ComponentModel.DataAnnotations.Schema;

namespace InversePropertyExample.Models;
// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/mapping-attributes#inversepropertyattribute
public class Blog {
    public int Id { get; set; }

    [InverseProperty("Blog")]
    public List<Post> Posts { get; } = new();

    [ForeignKey(nameof(FeaturedPost))]
    public int FeaturedPostId { get; set; }

    public Post FeaturedPost { get; set; }
}

public class Post {
    public int Id { get; set; }

    [ForeignKey(nameof(Blog))]
    public int BlogId { get; set; }

    public Blog Blog { get; init; }
}
