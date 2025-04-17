using AirportTicketBookingExercise.Domain.Entities;
using AirportTicketBookingExercise.Domain.Enums;
using AirportTicketBookingExercise.Infrastructure.Validation;

namespace AirportTicketBookingExercise.Infrastructure.Utilities;

public class FlightBookingsParser
{
    private readonly Dictionary<string, string> _validationRules;

    public FlightBookingsParser()
    {
        _validationRules = BookingFlightsMetadata.GetValidationRules();
    }
    internal Booking ParseBooking(string line)
    {
        var parts = line.Split(',');

        var errors = new List<string>();

        if (parts.Length < _validationRules.Count)
        {   
            errors.Add($"Invalid data format: missing required fields.");
            PrintErrors(line, errors);
            return null;
        }
        
        if (!int.TryParse(parts[0], out int bookingId))
            errors.Add($"Booking ID Error: {_validationRules["Booking ID"]}");

        if (!int.TryParse(parts[1], out int flightId))
            errors.Add($"Flight ID Error: {_validationRules["Flight ID"]}");

        if (!int.TryParse(parts[2], out int passengerId))
            errors.Add($"Passenger ID Error: {_validationRules["Passenger ID"]}");

        
        if (string.IsNullOrWhiteSpace(parts[3]))
            errors.Add($"Passenger Name Error: {_validationRules["Passenger Name"]}");


        if (!Enum.TryParse(typeof(SeatClass), parts[4], out var seatClass))
            errors.Add($"Seat Class Error: {_validationRules["Seat Class"]}");

        if (!decimal.TryParse(parts[5], out decimal price))
            errors.Add($"Price Error: {_validationRules["Price"]}");
        
        if (!DateTime.TryParse(parts[6], out DateTime bookingDate))
            errors.Add($"Booking Date Error: {_validationRules["Booking Date"]}");

        if (errors.Any())
        {
            PrintErrors(line, errors);
            return null;
        }

        return new Booking
        {
            BookingId = bookingId,
            FlightId = flightId,
            PassengerId = passengerId,
            PassengerName = parts[3],
            SeatClass = (SeatClass)seatClass,
            Price = price,
            BookingDate = bookingDate,
        };
    }
    private void PrintErrors(string line, List<string> errors)
    {
        Console.WriteLine($"Validation Errors for line: {line}");
        errors.ForEach(error => Console.WriteLine($"- {error}"));
    }
}