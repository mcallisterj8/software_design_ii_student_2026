using BugLab03.Models;
using Microsoft.EntityFrameworkCore;

namespace BugLab03.Data;

public class ApplicationDbContext : DbContext {
    public DbSet<Album> Album { get; set; }
    public DbSet<Artist> Artist { get; set; }
    public DbSet<Customer> Customer { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Genre> Genre { get; set; }
    public DbSet<Invoice> Invoice { get; set; }
    public DbSet<InvoiceLine> InvoiceLine { get; set; }
    public DbSet<MediaType> MediaType { get; set; }
    public DbSet<Playlist> Playlist { get; set; }
    public DbSet<Track> Track { get; set; }
    public DbSet<PlaylistTrack> PlaylistTrack { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=bug_lab_03.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Playlist>()
            .HasMany(pl => pl.Tracks)
            .WithMany(t => t.Playlists)
            .UsingEntity<PlaylistTrack>();
    }

}
