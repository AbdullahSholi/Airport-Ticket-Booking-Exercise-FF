using AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

public interface IBookAFlightUseCase
{
    public void BookAFlight(string bookingFlightsFilePath, Booking booking);
}