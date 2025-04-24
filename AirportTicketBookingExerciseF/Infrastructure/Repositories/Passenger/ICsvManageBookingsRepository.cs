using AirportTicketBookingExerciseF.Domain.Entities;

namespace AirportTicketBookingExerciseF.Infrastructure.Repositories.Passenger;
    
public interface ICsvManageBookingsRepository
{
    public void SaveBookings(List<Booking> bookings);

    public List<Booking> GetAllBookings();

}