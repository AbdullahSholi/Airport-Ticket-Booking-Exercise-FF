using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Infrastructure.Repositories.Passenger;

public interface ICsvSearchForAvailableFlightsRepository
{
    public List<Flight> SearchForAvailableFlights();

}