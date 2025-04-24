using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;

public interface ICsvBookAFlightRepository
{
    public void AppendBookingToCsv(string filePath, Booking booking);
}