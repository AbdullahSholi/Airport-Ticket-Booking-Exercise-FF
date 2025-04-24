using AirportTicketBookingExerciseF.Application.UseCasesImplementation.Manager;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.Enums;
using AirportTicketBookingExerciseF.Infrastructure.Repositories.Manager;
using Moq;

namespace Airport_Ticket_Booking_Exercise_F.Tests;

public class FilterBookingsServiceShould
{
    [Fact]
    public void FilterBookings_WhenBookingMatchingCriteria_ThenPrintFilteredBookings()
    {
        // Arrange
        var mockRepository = new Mock<ICsvFilterBookingsRepository>();
        var filterBookings = new FilterBookingsService(mockRepository.Object);
        
        var passengers = new List<Passenger>
        {
            new Passenger { PassengerId = 1, FirstName = "Harry", LastName = "Thomas", Email = "user1@example.com", PhoneNumber = "+1-555-8273"},
            new Passenger { PassengerId = 2, FirstName = "John", LastName = "Brown", Email = "user2@example.com", PhoneNumber = "+1-555-6240"}
        };

        var bookings = new List<Booking>
        {
            new Booking { BookingId = 1,PassengerId = 1, FlightId = 1, Price = 100, SeatClass = Enum.Parse<SeatClass>("Economy"), BookingDate = DateTime.Parse("2025-02-10 12:18:47")},
            new Booking { BookingId = 2, PassengerId = 2, FlightId = 2, Price = 200, SeatClass = Enum.Parse<SeatClass>("Business"), BookingDate = DateTime.Parse("2025-02-10 12:18:47") }
        };

        var flights = new List<Flight>
        {
            new Flight { FlightId = 1, DepartureCountry = "USA", DestinationCountry = "Canada", DepartureDate = DateTime.Now.AddDays(30), DepartureAirport = "JFK", ArrivalAirport = "YYZ" },
            new Flight { FlightId = 2, DepartureCountry = "Canada", DestinationCountry = "USA", DepartureDate = DateTime.Now.AddDays(10), DepartureAirport = "YYZ", ArrivalAirport = "LAX" }
        };

        mockRepository.Setup(r => r.GetBookings()).Returns(bookings);
        mockRepository.Setup(r => r.GetFlights()).Returns(flights);
        mockRepository.Setup(r => r.GetPassengers()).Returns(passengers);
        
        // This for redirect console result to sw variable
        using (var sw = new System.IO.StringWriter()) 
        {
            // Act
            Console.SetOut(sw);            
            filterBookings.FilterBookings("DepartureCountry", "USA");
            var output = sw.ToString();
            
            // Assert
            Assert.Contains("USA", output);
            Assert.DoesNotContain("Russia", output);
        }
        
    }

    [Fact]
    public void FilterBookings_WhenBookingNotMatchingCriteria_ThenPrintNoMatchingBookings()
    {
        // Arrange
        var mockRepository = new Mock<ICsvFilterBookingsRepository>();
        var filterBookings = new FilterBookingsService(mockRepository.Object);
        
        var passengers = new List<Passenger>
        {
            new Passenger { PassengerId = 1, FirstName = "Harry", LastName = "Thomas", Email = "user1@example.com", PhoneNumber = "+1-555-8273"},
            new Passenger { PassengerId = 2, FirstName = "John", LastName = "Brown", Email = "user2@example.com", PhoneNumber = "+1-555-6240"}
        };

        var bookings = new List<Booking>
        {
            new Booking { BookingId = 1,PassengerId = 1, FlightId = 1, Price = 100, SeatClass = Enum.Parse<SeatClass>("Economy"), BookingDate = DateTime.Parse("2025-02-10 12:18:47")},
            new Booking { BookingId = 2, PassengerId = 2, FlightId = 2, Price = 200, SeatClass = Enum.Parse<SeatClass>("Business"), BookingDate = DateTime.Parse("2025-02-10 12:18:47") }
        };

        var flights = new List<Flight>
        {
            new Flight { FlightId = 1, DepartureCountry = "USA", DestinationCountry = "Canada", DepartureDate = DateTime.Now.AddDays(30), DepartureAirport = "JFK", ArrivalAirport = "YYZ" },
            new Flight { FlightId = 2, DepartureCountry = "Canada", DestinationCountry = "USA", DepartureDate = DateTime.Now.AddDays(10), DepartureAirport = "YYZ", ArrivalAirport = "LAX" }
        };

        mockRepository.Setup(r => r.GetBookings()).Returns(bookings);
        mockRepository.Setup(r => r.GetFlights()).Returns(flights);
        mockRepository.Setup(r => r.GetPassengers()).Returns(passengers);

        using (var sw = new System.IO.StringWriter())
        {
            Console.SetOut(sw);
            // Act
            filterBookings.FilterBookings("DepartureCountry", "Palestine");
            var output = sw.ToString();
            
            // Assert
            Assert.DoesNotContain("Palestine", output);
        }
    }
}