using AirportTicketBookingExerciseF.Application.Utilities;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Manager;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Manager;

public class FilterBookingsService : IFilterBookingsService
{
    private readonly CsvFilterBookingsRepository _filterBookingsRepository;

    public FilterBookingsService(CsvFilterBookingsRepository filterBookingsRepository)
    {
        _filterBookingsRepository = filterBookingsRepository;
    }

    public void FilterBookings(string? parameter, string? value)
    {
        var passengers = _filterBookingsRepository.GetPassengers();

        var bookings = _filterBookingsRepository.GetBookings();

        var flights = _filterBookingsRepository.GetFlights();

        var results = from b in bookings
            join p in passengers on b.PassengerId equals p.PassengerId
            join f in flights on b.FlightId equals f.FlightId
            select new
            {
                f.FlightId,
                b.Price,
                f.DepartureCountry,
                f.DestinationCountry,
                f.DepartureDate,
                f.DepartureAirport,
                f.ArrivalAirport,
                p.FullName,
                b.SeatClass
            };

        var filteredBookings = results
            .Where(b => b.GetType().GetProperty(parameter)?.GetValue(b)?.ToString().Contains(value) == true).ToList();
        if (!filteredBookings.Any()) Console.WriteLine(Messages.NoMatchingBookings);
        else
            filteredBookings.ForEach(b =>
                Console.WriteLine(
                    $"Flight: {b.FlightId}, Price: {b.Price}, Departure Country: {b.DepartureCountry}, Destination Country: {b.DestinationCountry}, Departure Date: {b.DepartureDate}, Departure Airport: {b.ArrivalAirport}, Arrival Airport: {b.ArrivalAirport},  Passenger: {b.FullName}, Class: {b.SeatClass}"));
    }
}