using System.Globalization;
using AirportTicketBookingExercise.Domain.Entities;
using AirportTicketBookingExercise.Domain.Enums;
using AirportTicketBookingExercise.Domain.UseCasesDeclaration;
using AirportTicketBookingExercise.Infrastructure.Utilities;
using CsvHelper;

namespace AirportTicketBookingExercise.Infrastructure.Repositories;

public class CsvFilterBookingsRepository
{
    private readonly string _csvFilePathBookings;
    private readonly string _csvFilePathPassengers;
    private readonly string _csvFilePathFlights;
    private FlightBookingsParser _flightBookingsParser;
    private GetAllPassengersParser _passengersParser;
    private GetAllFlightsParser _flightsParser;
    

    public CsvFilterBookingsRepository(string csvFilePathBookings, string csvFilePathPassengers, string csvFilePathFlights, GetAllPassengersParser passengersParser,FlightBookingsParser flightBookingsParser, GetAllFlightsParser flightsParser)
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

        var lines = File.ReadAllLines(_csvFilePathBookings).Skip(1); // Skip header row
        return lines.Select(line => _flightBookingsParser.ParseBooking(line)).ToList();
    }

    public List<Passenger> GetPassengers()
    {
        if (!File.Exists(_csvFilePathPassengers)) return new List<Passenger>();

        var lines = File.ReadAllLines(_csvFilePathPassengers).Skip(1); // Skip header row
        return lines.Select(line => _passengersParser.ParsePassenger(line)).ToList();
    }
    
    public List<Flight> GetFlights()
    {
        if (!File.Exists(_csvFilePathFlights)) return new List<Flight>();

        var lines = File.ReadAllLines(_csvFilePathFlights).Skip(1); // Skip header row
        return lines.Select(line => _flightsParser.ParseFlights(line)).ToList();
    }

    

}
