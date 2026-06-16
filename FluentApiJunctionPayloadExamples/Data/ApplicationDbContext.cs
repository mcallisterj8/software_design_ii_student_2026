using Microsoft.EntityFrameworkCore;
using FluentApiJunctionPayloadExamples.Models;

namespace FluentApiJunctionPayloadExamples.Data;

public class ApplicationDbContext : DbContext {
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=fluentapiexamples.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Tags) // Post navigation property to Tags
            .WithMany(t => t.Posts) // Tag navigation property to Posts
            .UsingEntity<PostTag>(
                        // Defining the "left" table.
                        l => l.HasOne<Tag>() // Empty because no navigation property to Tag
                            .WithMany() // Empty because no navigation property to Posts
                            .HasForeignKey(pt => pt.Tid), // Foreign key used for Tag in PostTag
                                                          // Defining the "left" table.
                        r => r.HasOne<Post>() // Empty because no navigation property to Tag
                            .WithMany() // Empty because no navigation property to Tags
                            .HasForeignKey(pt => pt.Pid),
              // For the "payload" properties
              je => { // je is just standing for "join entity" here.
                  je.HasKey(pt => new { pt.Pid, pt.Tid }); // Required for non-conventional key names when there are multiple payload properties
                  je.Property(pt => pt.Notes);
                  je.Property(pt => pt.CreatedOn);
                  je.Property(pt => pt.IsActive);
              });
    }
}
