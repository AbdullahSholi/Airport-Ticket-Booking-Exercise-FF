using AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Repositories.Passenger;
using Moq;

namespace Airport_Ticket_Booking_Exercise_F.Tests;

public class SearchForAvailableFlightsServiceShould
{
    [Fact]
    public void SearchForAvailableFlights()
    {
        // Arrange
        var flights = new List<Flight>
        {
            new Flight { FlightId = 1, DepartureCountry = "UK", DestinationCountry = "France", DepartureDate = DateTime.Parse("2029-04-20 12:11:07"), DepartureAirport = "GRU", ArrivalAirport = "LHR", Price = (decimal)664.08 },
        };
        var mockedRepository = new Mock<ICsvSearchForAvailableFlightsRepository>();
        var searchForAvailableFlightsService = new SearchForAvailableFlightsService(mockedRepository.Object);
        mockedRepository.Setup(x => x.SearchForAvailableFlights()).Returns(flights);

        // Act
        var result = searchForAvailableFlightsService.SearchForAvailableFlights("FlightId", "1");
        
        // Assert
        Assert.Equal(flights, result);

    }
}