using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;

public class BookAFlightUseCase : IBookAFlightUseCase
{
    private readonly CsvBookAFlightRepository _bookAFlightRepository;

    public BookAFlightUseCase(CsvBookAFlightRepository bookAFlightRepository)
    {
        _bookAFlightRepository = bookAFlightRepository;
    }
    public void BookAFlight(string bookingFlightsFilePath, Booking booking)
    {
        _bookAFlightRepository.AppendBookingToCsv(bookingFlightsFilePath, booking);
    }
}