namespace AirportTicketBookingExerciseF.Domain.Entities;

public class Flight
{
    public int FlightId { get; set; }
    public string? DepartureCountry { get; set; }
    public string? DestinationCountry { get; set; }
    public DateTime DepartureDate { get; set; }
    public string? DepartureAirport { get; set; }
    public string? ArrivalAirport { get; set; }
    public decimal Price { get; set; }
}