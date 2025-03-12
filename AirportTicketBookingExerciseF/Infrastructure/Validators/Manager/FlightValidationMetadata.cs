namespace AirportTicketBookingExercise.Infrastructure.Validation;

using System.Collections.Generic;


public static class FlightValidationMetadata
{
    public static Dictionary<string, string> GetValidationRules()
    {
        return new Dictionary<string, string>
        {
            { "Flight ID", "Type: Integer, Constraint: Required" },
            { "Departure Country", "Type: Free Text, Constraint: Required" },
            { "Destination Country", "Type: Free Text, Constraint: Required" },
            { "Departure Date", "Type: Date Time, Constraint: Required, Allowed Range (today → future)" },
            { "Departure Airport", "Type: Free Text, Constraint: Required" },
            { "Arrival Airport", "Type: Free Text, Constraint: Required" },
            { "Price", "Type: Decimal, Constraint: Required" }
        };
    }
}
