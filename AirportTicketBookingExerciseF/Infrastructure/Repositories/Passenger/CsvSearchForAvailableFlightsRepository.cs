using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Repositories.Passenger;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Passenger;

public class CsvSearchForAvailableFlightsRepository : ICsvSearchForAvailableFlightsRepository
{
    private readonly string _csvFilePath;
    private readonly SearchForAvailableFlightsParser _searchForAvailableFlightsParser;

    public CsvSearchForAvailableFlightsRepository(string csvFilePath,
        SearchForAvailableFlightsParser searchForAvailableFlightsParser)
    {
        _csvFilePath = csvFilePath;
        _searchForAvailableFlightsParser = searchForAvailableFlightsParser;
    }

    public List<Flight> SearchForAvailableFlights()
    {
        if (!File.Exists(_csvFilePath)) return new List<Flight>();
        
        var lines = File.ReadAllLines(_csvFilePath).Skip(1);
        return lines.Select(line => _searchForAvailableFlightsParser.ParseAvailableFlights(line)).ToList();
    }
}