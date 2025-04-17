using AirportTicketBookingExercise.Application.UseCasesImplementation.Passenger;
using AirportTicketBookingExercise.Domain.Entities;

namespace AirportTicketBookingExercise.Domain.UseCasesDeclaration.Passenger;

public interface IBookAFlightUseCase
{
    public void BookAFlight(string bookingFlightsFilePath, Booking booking);
}