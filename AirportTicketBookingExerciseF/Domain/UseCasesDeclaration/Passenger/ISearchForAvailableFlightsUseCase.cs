using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

public interface ISearchForAvailableFlightsUseCase
{
    public List<Flight> SearchForAvailableFlights(string? parameter, string? value);

}