namespace AirportTicketBookingExerciseF.Infrastructure.Validation;

public class PassengersMetadata
{
    public static Dictionary<string, string> GetValidationRules()
    {
        return new Dictionary<string, string>
        {
            { "Passenger ID", "Type: Integer, Constraint: Required" },
            { "First Name", "Type: Free Text, Constraint: Required" },
            { "Last Name", "Type: Free Text, Constraint: Required" },
            { "Email", "Type: Free Text, Constraint: Required" },
            { "Phone Number", "Type: Free Text, Constraint: Required" }
        };
    }
}