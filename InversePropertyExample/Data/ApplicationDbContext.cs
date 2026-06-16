using Microsoft.EntityFrameworkCore;
using InversePropertyExample.Models;

// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/mapping-attributes#inversepropertyattribute

namespace FluentApiJunctionPayloadExamples.Data;

public class ApplicationDbContext : DbContext {
    public DbSet<Post> Posts { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=inverse_property_example.db");


}
