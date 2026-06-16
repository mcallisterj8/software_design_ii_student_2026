using Microsoft.Extensions.DependencyInjection;
using BugLab01.Data;
using BugLab01.Services;

ServiceProvider _serviceProvider;
MusicQueryService _musicQueryService;

// Create service collection
var services = new ServiceCollection();

services.AddDbContext<ApplicationDbContext>();
services.AddScoped<MusicQueryService>();

// Build services
_serviceProvider = services.BuildServiceProvider();

// Retrieve the MusicQueryService
_musicQueryService = _serviceProvider.GetRequiredService<MusicQueryService>();