using AirportTicketBookingExercise.Domain.Enums;

namespace AirportTicketBookingExercise.Domain.Entities;

public class Booking
{
    public int BookingId { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public string? PassengerName { get; set; }
    public SeatClass? SeatClass { get; set; }
    public decimal Price { get; set; }
    public DateTime BookingDate { get; set; }
    
}