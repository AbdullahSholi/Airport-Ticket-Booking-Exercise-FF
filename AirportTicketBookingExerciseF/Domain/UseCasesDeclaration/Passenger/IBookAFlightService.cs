using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

public interface IBookAFlightService
{
    public void BookAFlight(string bookingFlightsFilePath, Booking booking);
}