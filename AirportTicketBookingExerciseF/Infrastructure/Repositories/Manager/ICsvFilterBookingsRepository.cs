using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Infrastructure.Repositories.Manager;

public interface ICsvFilterBookingsRepository
{
    public List<Booking> GetBookings();

    public List<Domain.Entities.Passenger> GetPassengers();

    public List<Flight> GetFlights();

}