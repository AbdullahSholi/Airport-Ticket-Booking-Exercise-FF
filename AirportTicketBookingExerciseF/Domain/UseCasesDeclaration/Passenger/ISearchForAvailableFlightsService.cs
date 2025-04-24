using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

public interface ISearchForAvailableFlightsService
{
    public List<Flight> SearchForAvailableFlights(string? parameter, string? value);
}