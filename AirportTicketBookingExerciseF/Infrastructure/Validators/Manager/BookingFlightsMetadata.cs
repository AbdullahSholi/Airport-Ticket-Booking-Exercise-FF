namespace AirportTicketBookingExerciseF.Infrastructure.Validators.Manager;

public class BookingFlightsMetadata
{
    public static Dictionary<string, string> GetValidationRules()
    {
        return new Dictionary<string, string>
        {
            { "Booking ID", "Type: Integer, Constraint: Required" },
            { "Flight ID", "Type: Free Text, Constraint: Required" },
            { "Passenger ID", "Type: Free Text, Constraint: Required" },
            { "Passenger Name", "Type: Free Text, Constraint: Required" },
            { "Seat Class", "Type: Enum, Constraint: Required (Economy, Business, First Class)" },
            { "Price", "Type: Decimal, Constraint: Required" },
            { "Booking Date", "Type: Date Time, Constraint: Required, Allowed Range (today \u2192 future)" },
            
        };
    }
}