using AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.Enums;
using Moq;

namespace Airport_Ticket_Booking_Exercise_F.Tests;

public class BookAFlightServiceShould
{
    [Fact]
    public void BookAFlight_WhenEnterValidData_ShouldAppendBookingToCsv()
    {
        // Arrange
        var mockedRepository = new Mock<ICsvBookAFlightRepository>();
        var bookAFlightService = new BookAFlightService(mockedRepository.Object);
        var filePath = @"C:\Users\abdul\RiderProjects\Airport-Ticket-Booking-Exercise-FF\AirportTicketBookingExerciseF\Infrastructure\FileData\Bookings.csv";
        var bookings = new Booking()
        {
            BookingId = 100, PassengerId = 1, FlightId = 1, Price = 100,
            SeatClass = Enum.Parse<SeatClass>("Economy"), BookingDate = DateTime.Parse("2025-02-10 12:18:47")
        };
        
        // Act
        bookAFlightService.BookAFlight(filePath, bookings);
        
        // Assert
        mockedRepository.Verify(x => x.AppendBookingToCsv(filePath, bookings), Times.Once);
    }
}