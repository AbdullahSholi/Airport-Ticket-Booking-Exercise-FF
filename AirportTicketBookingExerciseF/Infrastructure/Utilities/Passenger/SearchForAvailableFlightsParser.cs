using System.Globalization;
using AirportTicketBookingExercise.Domain.Entities;
using AirportTicketBookingExercise.Domain.Enums;
using AirportTicketBookingExercise.Infrastructure.Validation;

namespace AirportTicketBookingExercise.Infrastructure.Utilities;

public class SearchForAvailableFlightsParser
{
    private readonly Dictionary<string, string> _validationRules;

    public SearchForAvailableFlightsParser()
    {
        _validationRules = SearchForAvailableFlightsMetadata.GetValidationRules();
    }
    internal Flight ParseAvailableFlights(string line)
    {
        var parts = line.Split(',');

        var errors = new List<string>();

        if (parts.Length < _validationRules.Count)
        {   
            errors.Add($"Invalid data format: missing required fields.");
            PrintErrors(line, errors);
            return null;
        }
        
        if (!int.TryParse(parts[0], out int flightId))
            errors.Add($"Flight ID Error: {_validationRules["Flight ID"]}");

        if (string.IsNullOrWhiteSpace(parts[1]))
            errors.Add($"Departure Country Error: {_validationRules["Departure Country"]}");

        if (string.IsNullOrWhiteSpace(parts[2]))
            errors.Add($"Destination Country Error: {_validationRules["Destination Country"]}");

        if (!DateTime.TryParseExact(parts[3], "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime departureDate) || departureDate < DateTime.Today)
            errors.Add($"Departure Date Error: {_validationRules["Departure Date"]}");

        if (string.IsNullOrWhiteSpace(parts[4]))
            errors.Add($"Departure Airport Error: {_validationRules["Departure Airport"]}");

        if (string.IsNullOrWhiteSpace(parts[5]))
            errors.Add($"Arrival Airport Error: {_validationRules["Arrival Airport"]}");

        if (!decimal.TryParse(parts[6], out decimal price))
            errors.Add($"Price Error: {_validationRules["Price"]}");

        if (errors.Any())
        {
            PrintErrors(line, errors);
            return null;
        }

        return new Flight
        {
            FlightId = flightId,
            DepartureCountry = parts[1],
            DestinationCountry= parts[2],
            DepartureDate= departureDate,
            DepartureAirport= parts[4],
            ArrivalAirport= parts[5],
            Price = price
        };
    }
    private void PrintErrors(string line, List<string> errors)
    {
        Console.WriteLine($"Validation Errors for line: {line}");
        errors.ForEach(error => Console.WriteLine($"- {error}"));
    }
}