using AirportTicketBookingExerciseF.Application.UseCasesImplementation.Manager;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Repositories.Manager;
using Moq;
using Xunit.Abstractions;

namespace Airport_Ticket_Booking_Exercise_F.Tests;

public class GetAllFlightsServiceShould
{
    [Fact]
    public void GetAllFlights_WhenGetAllFlightsIsCalled_ThenAllFlightsShouldBeReturned()
    {
        // Arrange
        var mockedRepository = new Mock<ICsvGetAllFlightsRepository>();
        var flightService = new GetAllFlightsService(mockedRepository.Object);
        var expectedFlights = new List<Flight>
        {
            new Flight
            {
                FlightId = 1, DepartureCountry = "USA", DestinationCountry = "Canada",
                DepartureDate = DateTime.Now.AddDays(30), DepartureAirport = "JFK", ArrivalAirport = "YYZ"
            },
            new Flight
            {
                FlightId = 2, DepartureCountry = "Canada", DestinationCountry = "USA",
                DepartureDate = DateTime.Now.AddDays(10), DepartureAirport = "YYZ", ArrivalAirport = "LAX"
            }
        };
        mockedRepository.Setup(x => x.GetAllFlights()).Returns(expectedFlights);
            

    using (var sw = new System.IO.StringWriter())
        {
            Console.SetOut(sw);
            // Act
            flightService.GetAllFlights();
            var output = sw.ToString();
            
            // Assert
            Assert.Contains("JFK", output);
            Assert.Contains("USA", output);
        }
    }
}