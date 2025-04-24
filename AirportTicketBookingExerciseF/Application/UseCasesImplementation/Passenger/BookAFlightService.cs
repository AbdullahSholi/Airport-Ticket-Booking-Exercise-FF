using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;

public class BookAFlightService : IBookAFlightService
{
    private readonly ICsvBookAFlightRepository _bookAFlightRepository;

    public BookAFlightService(ICsvBookAFlightRepository bookAFlightRepository)
    {
        _bookAFlightRepository = bookAFlightRepository;
    }

    public void BookAFlight(string bookingFlightsFilePath, Booking booking)
    {
        _bookAFlightRepository.AppendBookingToCsv(bookingFlightsFilePath, booking);
    }
}