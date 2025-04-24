using AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.Enums;
using AirportTicketBookingExerciseF.Infrastructure.Repositories.Passenger;
using Moq;
using Xunit.Abstractions;

namespace Airport_Ticket_Booking_Exercise_F.Tests;

public class ManageBookingsServiceShould
{
    private readonly ITestOutputHelper _output;

    public ManageBookingsServiceShould(ITestOutputHelper output)
    {
        _output = output;
    }
    [Fact]
    public void ViewPersonalBookings()
    {
        var mockedRepository = new Mock<ICsvManageBookingsRepository>();
        var manageBookingService = new ManageBookingsService(mockedRepository.Object);
        var bookings = new List<Booking>
        {
            new Booking { BookingId = 1,PassengerId = 1, FlightId = 1, Price = 100, SeatClass = Enum.Parse<SeatClass>("Economy"), BookingDate = DateTime.Parse("2025-02-10 12:18:47")},
            new Booking { BookingId = 2, PassengerId = 2, FlightId = 2, Price = 200, SeatClass = Enum.Parse<SeatClass>("Business"), BookingDate = DateTime.Parse("2025-02-10 12:18:47") }
        };

        mockedRepository.Setup(x => x.GetAllBookings()).Returns(bookings);
        
        using (var input = new StringReader("1"))
        using (var sw = new StringWriter())
        {
            Console.SetIn(input);
            Console.SetOut(sw);
            
            // Act
            manageBookingService.ViewPersonalBookings();
            var result = sw.ToString();
            _output.WriteLine(result);
            
            // Assert
            Assert.Contains("Id:", result);
            Assert.Contains("FlightId:", result);
            Assert.Contains("PassengerId:", result);
        }
    }

    [Fact]
    public void CancelABooking()
    {
        var mockedRepository = new Mock<ICsvManageBookingsRepository>();
        var manageBookingService = new ManageBookingsService(mockedRepository.Object);
        var bookings = new List<Booking>
        {
            new Booking { BookingId = 1,PassengerId = 1, FlightId = 1, Price = 100, SeatClass = Enum.Parse<SeatClass>("Economy"), BookingDate = DateTime.Parse("2025-02-10 12:18:47")},
            new Booking { BookingId = 2, PassengerId = 2, FlightId = 2, Price = 200, SeatClass = Enum.Parse<SeatClass>("Business"), BookingDate = DateTime.Parse("2025-02-10 12:18:47") }
        };

        mockedRepository.Setup(x => x.GetAllBookings()).Returns(bookings);
        
        using (var input = new StringReader("1\n1"))
        using (var sw = new StringWriter())
        {
            Console.SetIn(input);
            Console.SetOut(sw);
            
            // Act 
            manageBookingService.CancelABooking();
            var result = sw.ToString();
            _output.WriteLine(result);
            
            Assert.Contains("PassengerId: 1", result);
        }
    }

    [Fact]
    public void ModifyABooking()
    {
        // Arrange
        var mockedRepository = new Mock<ICsvManageBookingsRepository>();
        var manageBookingService = new ManageBookingsService(mockedRepository.Object);
        var bookings = new List<Booking>
        {
            new Booking { BookingId = 1,PassengerId = 1, FlightId = 1, Price = 100, SeatClass = Enum.Parse<SeatClass>("Economy"), BookingDate = DateTime.Parse("2025-02-10 12:18:47")},
            new Booking { BookingId = 2, PassengerId = 2, FlightId = 2, Price = 200, SeatClass = Enum.Parse<SeatClass>("Business"), BookingDate = DateTime.Parse("2025-02-10 12:18:47") }
        };

        mockedRepository.Setup(x => x.GetAllBookings()).Returns(bookings);
        
        using (var input = new StringReader("1\n1\nEconomy\n2025-02-10 12:18:47"))
        using (var sw = new StringWriter())
        {
            Console.SetIn(input);
            Console.SetOut(sw);
            
            // Act 
            manageBookingService.ModifyABooking();
            var result = sw.ToString();
            _output.WriteLine(result);
            
            // Assert
            Assert.Contains("PassengerId: 1", result);
            Assert.Contains("FlightId: 1", result);
            Assert.Contains("Enter the booking Id:", result);
            Assert.Contains("Enter new SeatClass:", result);
            Assert.Contains("Enter new Booking Date:", result);
        }
    }
}