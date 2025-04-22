namespace AirportTicketBookingExerciseF.Domain.Entities;

public class Passenger
{
    public int PassengerId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FullName => $"{FirstName} {LastName}";
}