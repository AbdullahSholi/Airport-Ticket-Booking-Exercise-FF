using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Validation;

public class GetAllPassengersParser
{
    private readonly Dictionary<string, string> _validationRules;

    public GetAllPassengersParser()
    {
        _validationRules = PassengersMetadata.GetValidationRules();
    }
    internal Passenger ParsePassenger(string line)
    {
        var parts = line.Split(',');

        var errors = new List<string>();

        if (parts.Length < _validationRules.Count)
        {   
            errors.Add($"Invalid data format: missing required fields.");
            PrintErrors(line, errors);
            return null;
        }
        
        if (!int.TryParse(parts[0], out int passengerId))
            errors.Add($"Passenger ID Error: {_validationRules["Passenger ID"]}");
        
        
        if (string.IsNullOrWhiteSpace(parts[1]))
            errors.Add($"Passenger First Name Error: {_validationRules["First Name"]}");

        if (string.IsNullOrWhiteSpace(parts[2]))
            errors.Add($"Passenger Last Name Error: {_validationRules["Last Name"]}");
        
        if (string.IsNullOrWhiteSpace(parts[3]))
            errors.Add($"Passenger Email Error: {_validationRules["Email"]}");

        if (string.IsNullOrWhiteSpace(parts[4]))
            errors.Add($"Passenger PhoneNumber Error: {_validationRules["Phone Number"]}");
        

        if (errors.Any())
        {
            PrintErrors(line, errors);
            return null;
        }

        return new Passenger
        {
            PassengerId = passengerId,
            FirstName = parts[1],
            LastName = parts[2],
            Email = parts[3],
            PhoneNumber = parts[4]
        };
    }
    private void PrintErrors(string line, List<string> errors)
    {
        Console.WriteLine($"Validation Errors for line: {line}");
        errors.ForEach(error => Console.WriteLine($"- {error}"));
    }
}