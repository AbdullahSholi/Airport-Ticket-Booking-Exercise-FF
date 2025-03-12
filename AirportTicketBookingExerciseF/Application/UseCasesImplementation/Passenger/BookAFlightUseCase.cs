using AirportTicketBookingExercise.Domain.Entities;
using AirportTicketBookingExercise.Domain.UseCasesDeclaration.Passenger;
using AirportTicketBookingExercise.Infrastructure.Repositories;

namespace AirportTicketBookingExercise.Application.UseCasesImplementation.Passenger;

public class BookAFlightUseCase : IBookAFlightUseCase
{
    public readonly CsvBookAFlightRepository _bookAFlightRepository;

    public BookAFlightUseCase(CsvBookAFlightRepository bookAFlightRepository)
    {
        _bookAFlightRepository = bookAFlightRepository;
    }
    public void BookAFlight(string bookingFlightsFilePath, Booking booking)
    {
        _bookAFlightRepository.AppendBookingToCsv(bookingFlightsFilePath, booking);
    }
}