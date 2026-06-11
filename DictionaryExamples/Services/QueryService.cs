using DictionaryExamples.Data;
using DictionaryExamples.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DictionaryExamples.Services;

public class QueryService {
    private readonly ApplicationDbContext _context;

    public QueryService(ApplicationDbContext context) {
        _context = context;
    }


    public async Task<Dictionary<string, List<Track>>> GetTrackNamesGroupedByGenreAsync() {
        return await _context.Track
            .GroupBy(t => t.Genre.Name) // Group tracks by genre name
            .ToDictionaryAsync(
                group => group.Key, // Genre name for the key
                group => group.ToList() // List of tracks in that genre for the value
            );
    }

    public async Task<Dictionary<string, List<Customer>>> GetCustomersByCityState() {
        /*
            1) The below approach fails because string interpolation becomes string.Format(),
            and EF Core cannot translate that string.Format() call inside the GroupBy().
            The null-coalescing operator itself is not the main problem.
        */
        // Dictionary<string, List<Customer>> customersByCityState = await _context.Customer
        //     .GroupBy(customer => $"{customer.City}, {customer.State ?? "NO STATE"}") // Value is a format string with the string interpolation operator ($).
        //     .ToDictionaryAsync(
        //         group => group.Key,
        //         group => group.ToList()
        //     );


        /*
            2) This approach groups by separate database-friendly values first.
            EF Core can translate City and State ?? "NO STATE" into SQL.
            After ToListAsync(), we build the formatted string key in regular C#.
        */
        var groupedCustomers = await _context.Customer
            .GroupBy(customer => new {
                City = customer.City,
                State = customer.State ?? "NO STATE" // Not using a format string.
            })
            .ToListAsync();

        Dictionary<string, List<Customer>> customersByCityState = groupedCustomers
            .ToDictionary(
                group => $"{group.Key.City}, {group.Key.State}",
                group => group.ToList()
            );

        return customersByCityState;
    }


    public async Task<Dictionary<string, List<Employee>>> GetEmployeesByTitleAsync() {
        return await _context.Employee
            .Where(employee => null != employee.Title)
            .GroupBy(employee => employee.Title)
            .ToDictionaryAsync(
                group => group.Key,
                group => group.ToList()
            );
    }

    public async Task<Dictionary<string, List<string>>> GetEmployeeNamesByTitleAsync() {
        return await _context.Employee
            .Where(employee => null != employee.Title)
            .GroupBy(employee => employee.Title)
            .ToDictionaryAsync(
                group => group.Key,
                group => group.Select(e => e.LastName).ToList()
            );
    }

    public async Task<Dictionary<string, List<Track>>> GetTracksByMediaTypeAsync() {
        return await _context.Track
            .Where(track => null != track.MediaType)
            .GroupBy(track => track.MediaType.Name)
            .ToDictionaryAsync(
                group => group.Key,
                group => group.ToList()
            );
    }

    public async Task<Dictionary<string, List<string>>> GetTrackNamesByGenreAsync() {
        return await _context.Track
            .Where(track => null != track.Genre)
            .GroupBy(track => track.Genre.Name)
            .ToDictionaryAsync(
                group => group.Key,
                group => group
                    .Select(track => track.Name)
                    .ToList()
            );
    }

    public async Task<Dictionary<string, double>> GetAverageTrackLengthSecondsByGenreAsync() {
        return await _context.Track
            .Where(track => null != track.Genre)
            .GroupBy(track => track.Genre.Name)
            .ToDictionaryAsync(
                group => group.Key,
                group => group.Average(track => track.Milliseconds) / 1000.0
            );
    }

    public async Task<Dictionary<string, int>> GetTrackCountByMediaTypeAsync() {
        return await _context.Track
            .Where(track => null != track.MediaType)
            .GroupBy(track => track.MediaType.Name)
            .ToDictionaryAsync(
                group => group.Key,
                group => group.Count()
            );
    }
}