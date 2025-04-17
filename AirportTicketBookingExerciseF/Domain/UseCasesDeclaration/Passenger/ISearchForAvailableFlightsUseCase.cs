using AirportTicketBookingExercise.Domain.Entities;

namespace AirportTicketBookingExercise.Domain.UseCasesDeclaration.Passenger;

public interface ISearchForAvailableFlightsUseCase
{
    public List<Flight> SearchForAvailableFlights(string? parameter, string? value);

}