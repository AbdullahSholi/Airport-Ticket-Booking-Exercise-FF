using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Repositories.Manager;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Manager;

public class CsvFilterBookingsRepository : ICsvFilterBookingsRepository
{
    private readonly string _csvFilePathBookings;
    private readonly string _csvFilePathFlights;
    private readonly string _csvFilePathPassengers;
    private readonly FlightBookingsParser _flightBookingsParser;
    private readonly GetAllFlightsParser _flightsParser;
    private readonly GetAllPassengersParser _passengersParser;


    public CsvFilterBookingsRepository(string csvFilePathBookings, string csvFilePathPassengers,
        string csvFilePathFlights, GetAllPassengersParser passengersParser, FlightBookingsParser flightBookingsParser,
        GetAllFlightsParser flightsParser)
    {
        _csvFilePathBookings = csvFilePathBookings;
        _csvFilePathPassengers = csvFilePathPassengers;
        _csvFilePathFlights = csvFilePathFlights;
        _flightBookingsParser = flightBookingsParser;
        _passengersParser = passengersParser;
        _flightsParser = flightsParser;
    }

    public List<Booking> GetBookings()
    {
        if (!File.Exists(_csvFilePathBookings)) return new List<Booking>();

        var lines = File.ReadAllLines(_csvFilePathBookings).Skip(1);
        return lines.Select(line => _flightBookingsParser.ParseBooking(line)).ToList();
    }

    public List<Passenger> GetPassengers()
    {
        if (!File.Exists(_csvFilePathPassengers)) return new List<Passenger>();

        var lines = File.ReadAllLines(_csvFilePathPassengers).Skip(1);
        return lines.Select(line => _passengersParser.ParsePassenger(line)).ToList();
    }

    public List<Flight> GetFlights()
    {
        if (!File.Exists(_csvFilePathFlights)) return new List<Flight>();

        var lines = File.ReadAllLines(_csvFilePathFlights).Skip(1);
        return lines.Select(line => _flightsParser.ParseFlights(line)).ToList();
    }
}