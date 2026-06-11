using Microsoft.Extensions.DependencyInjection;
using DictionaryExamples.Data;
using DictionaryExamples.Services;
using System.Text.Json;

ServiceProvider _serviceProvider;
QueryService _queryService;

// Create service collection
var services = new ServiceCollection();

services.AddScoped<QueryService>();
services.AddDbContext<ApplicationDbContext>();

// Build services
_serviceProvider = services.BuildServiceProvider();

// Get QueryService from DI container
_queryService = _serviceProvider.GetRequiredService<QueryService>();

/******************************************
    USING THE QUERY SERVICE
******************************************/

JsonSerializerOptions options = new JsonSerializerOptions {
    WriteIndented = true
};


// Tracks grouped by genre
Console.WriteLine("========== TRACKS GROUPED 1) BY GENRE ==========\n");

var tracksGroupedByGenre = await _queryService.GetTrackNamesGroupedByGenreAsync();

Console.WriteLine(JsonSerializer.Serialize(tracksGroupedByGenre, options));


// Customers grouped by city/state
Console.WriteLine("\n========== 2) CUSTOMERS GROUPED BY CITY/STATE ==========\n");

var customersByCityState = await _queryService.GetCustomersByCityState();

Console.WriteLine(JsonSerializer.Serialize(customersByCityState, options));


// Employees grouped by title
Console.WriteLine("\n========== 3) EMPLOYEES GROUPED BY TITLE ==========\n");

var employeesByTitle = await _queryService.GetEmployeesByTitleAsync();

Console.WriteLine(JsonSerializer.Serialize(employeesByTitle, options));


// Tracks grouped by media type
Console.WriteLine("\n========== 4) TRACKS GROUPED BY MEDIA TYPE ==========\n");

var tracksByMediaType = await _queryService.GetTracksByMediaTypeAsync();

Console.WriteLine(JsonSerializer.Serialize(tracksByMediaType, options));


// Track names grouped by genre
Console.WriteLine("\n========== 5) TRACK NAMES GROUPED BY GENRE ==========\n");

var trackNamesByGenre = await _queryService.GetTrackNamesByGenreAsync();

Console.WriteLine(JsonSerializer.Serialize(trackNamesByGenre, options));


// Average track length in seconds by genre
Console.WriteLine("\n========== 6) AVERAGE TRACK LENGTH SECONDS BY GENRE ==========\n");

var averageTrackLengthByGenre = await _queryService.GetAverageTrackLengthSecondsByGenreAsync();

Console.WriteLine(JsonSerializer.Serialize(averageTrackLengthByGenre, options));


// Track count by media type
Console.WriteLine("\n========== 7) TRACK COUNT BY MEDIA TYPE ==========\n");

var trackCountByMediaType = await _queryService.GetTrackCountByMediaTypeAsync();

Console.WriteLine(JsonSerializer.Serialize(trackCountByMediaType, options));