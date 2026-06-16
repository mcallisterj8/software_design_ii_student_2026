using Microsoft.EntityFrameworkCore;
using FluentApiExamples.Models;

namespace FluentApiExamples.Data;

public class ApplicationDbContext : DbContext {
    public DbSet<Post> Posts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=fluentapiexamples.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        /*
            Configuration for CONVENTIONAL key names in PostTag junction entity.

            Comment and uncomment appropriate blocks
            in the PostTag model to have EF Core setup database as intended.
        */
        // Make sure to comment out initially & show the error this causes.
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Tags) // Navigation property from Post to list of Tags
            .WithMany(t => t.Posts) // Navigation property from Tag to list of Posts
            .UsingEntity<PostTag>(); // Junction table used for many-to-many is PostTag


        /*
            Configuration for UNCONVENTIONAL key names in PostTag junction entity.

            Comment and uncomment appropriate blocks
            in the PostTag model to have EF Core setup database as intended.
        */
        // With non-conventional keys for junction table.
        // modelBuilder.Entity<Post>()
        //     .HasMany(p => p.Tags)
        //     .WithMany(t => t.Posts)
        //     .UsingEntity<PostTag>(
        //         // Defining the "left" table.
        //         l => l.HasOne<Tag>() // Empty because no navigation property to Tag
        //             .WithMany() // Empty because no navigation property to Posts
        //             .HasForeignKey(pt => pt.Tid), // Foreign key used for Tag in PostTag
        //         // Defining the "right" table.
        //         r => r.HasOne<Post>() // Empty because no navigation property to Tag
        //             .WithMany() // Empty because no navigation property to Tags
        //             .HasForeignKey(pt => pt.Pid)); // Foreign key used for Post in PostTag
    }
}
