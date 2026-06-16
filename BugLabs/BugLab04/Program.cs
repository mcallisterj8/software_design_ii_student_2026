using Microsoft.Extensions.DependencyInjection;
using BugLab04.Data;
using BugLab04.Models;
using BugLab04.Services;

ServiceProvider _serviceProvider;
MusicQueryService _musicQueryService;

// Create service collection
var services = new ServiceCollection();

services.AddScoped<MusicQueryService>();

// Build services
_serviceProvider = services.BuildServiceProvider();

// Retrieve the MusicQueryService
_musicQueryService = _serviceProvider.GetRequiredService<MusicQueryService>();

Console.WriteLine("\n--- GetAllArtistsWithAlbums ---");
var artistsWithAlbums = await _musicQueryService.GetAllArtistsWithAlbums();
foreach (var artist in artistsWithAlbums) {
    Console.WriteLine($"{artist.Name} ({artist.Albums.Count} albums)");
}

Console.WriteLine("\n--- GetAllArtistsWithMoreThanOneAlbum ---");
var artistsWithMoreThanOneAlbum = await _musicQueryService.GetAllArtistsWithMoreThanOneAlbum();
foreach (var artist in artistsWithMoreThanOneAlbum) {
    Console.WriteLine($"{artist.Name} ({artist.Albums.Count} albums)");
}

Console.WriteLine("\n--- GetArtistByNameWithAlbums ---");
var artistByName = await _musicQueryService.GetArtistByNameWithAlbums("AC/DC");
Console.WriteLine(artistByName != null
    ? $"{artistByName.Name} has {artistByName.Albums.Count} albums"
    : "Artist not found");

Console.WriteLine("\n--- GetTracksByAlbumId (AlbumId = 1) ---");
var tracksByAlbum = await _musicQueryService.GetTracksByAlbumId(1);
foreach (var track in tracksByAlbum) {
    Console.WriteLine(track.Name);
}

Console.WriteLine("\n--- GetAllGenresWithTracks ---");
var genresWithTracks = await _musicQueryService.GetAllGenresWithTracks();
foreach (var genre in genresWithTracks) {
    Console.WriteLine($"{genre.Name} ({genre.Tracks.Count} tracks)");
}

Console.WriteLine("\n--- GetTracksByGenreId (GenreId = 1) ---");
var tracksByGenre = await _musicQueryService.GetTracksByGenreId(1);
foreach (var track in tracksByGenre) {
    Console.WriteLine(track.Name);
}
