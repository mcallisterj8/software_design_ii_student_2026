using BugLab05.Data;
using Microsoft.EntityFrameworkCore;
using BugLab05.Models;

namespace BugLab05.Services;

public class MusicQueryService {
    private readonly ApplicationDbContext _context;

    public MusicQueryService(ApplicationDbContext context) {
        _context = context;
    }

    public async Task<List<Artist>> GetAllArtistsWithAlbums() {
        return await _context.Artist
            .Include(artist => artist.Albums)
            .Where(artist => artist.Albums.Count > 0)
            .ToListAsync();
    }

    public async Task<List<Artist>> GetAllArtistsWithMoreThanOneAlbum() {
        return await _context.Artist
            .Include(artist => artist.Albums)
            .Where(a => a.Albums.Count > 1)
            .ToListAsync();
    }

    public async Task<Artist?> GetArtistByNameWithAlbums(string artistName) {
        return await _context.Artist
            .Include(artist => artist.Albums)
            .FirstOrDefaultAsync(artist => artist.Name == artistName);
    }

    public async Task<List<Track>> GetTracksByAlbumId(int albumId) {
        return await _context.Track
            .Where(track => track.AlbumId == albumId)
            .ToListAsync();
    }

    public async Task<List<Genre>> GetAllGenresWithTracks() {
        return await _context.Genre
            .Include(genre => genre.Tracks)
            .ToListAsync();
    }

    public async Task<List<Track>> GetTracksByGenreId(int genreId) {
        return await _context.Track
            .Where(track => track.GenreId == genreId)
            .ToListAsync();
    }

}
