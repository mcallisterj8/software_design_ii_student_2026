using BugLab05.Models;
using Microsoft.EntityFrameworkCore;

namespace BugLab05.Data;

public class ApplicationDbContext : DbContext {
    public DbSet<Album> Album;
    public DbSet<Artist> Artist;
    public DbSet<Customer> Customer;
    public DbSet<Employee> Employee;
    public DbSet<Genre> Genre;
    public DbSet<Invoice> Invoice;
    public DbSet<InvoiceLine> InvoiceLine;
    public DbSet<MediaType> MediaType;
    public DbSet<Playlist> Playlist;
    public DbSet<Track> Track;
    public DbSet<PlaylistTrack> PlaylistTrack;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=bug_lab_05.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Playlist>()
            .HasMany(pl => pl.Tracks)
            .WithMany(t => t.Playlists)
            .UsingEntity<PlaylistTrack>();
    }

}
