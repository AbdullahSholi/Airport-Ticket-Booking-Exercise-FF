using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Utilities;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Manager;


public class CsvGetAllFlightsRepository
{
    private readonly string _csvFilePath;
    private readonly GetAllFlightsParser _getAllFlightsParser;

    public CsvGetAllFlightsRepository(string csvFilePath, GetAllFlightsParser getAllFlightsParser)
    {
        _csvFilePath = csvFilePath;
        _getAllFlightsParser = getAllFlightsParser;
    }
    
    public List<Flight> GetAllFlights()
    {
        if (!File.Exists(_csvFilePath)) return new List<Flight>();
        

        var lines = File.ReadAllLines(_csvFilePath).Skip(1); 
        return lines.Select(line => _getAllFlightsParser.ParseFlights(line)).ToList();
    }
}