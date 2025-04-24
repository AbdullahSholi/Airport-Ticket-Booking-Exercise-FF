using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Infrastructure.Repositories.Manager;

public interface ICsvGetAllFlightsRepository
{
    public List<Flight> GetAllFlights();
}