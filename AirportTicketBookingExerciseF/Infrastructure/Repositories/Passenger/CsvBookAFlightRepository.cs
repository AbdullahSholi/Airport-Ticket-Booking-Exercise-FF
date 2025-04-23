using System.Globalization;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Utilities;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Manager;

public class CsvBookAFlightRepository
{
    private readonly string? _csvFilePath;
    private readonly FlightBookingsParser _flightBookingsParser;

    public CsvBookAFlightRepository(string? csvFilePath, FlightBookingsParser flightBookingsParser)
    {
        _csvFilePath = csvFilePath;
        _flightBookingsParser = flightBookingsParser;
    }

    public void AppendBookingToCsv(string filePath, Booking booking)
    {
        var fileExists = File.Exists(filePath);

        using (var writer = new StreamWriter(filePath, true))
        {
            if (!fileExists)
                writer.WriteLine(Messages.CsvBookingHeader);

            writer.WriteLine(
                $"{booking.BookingId},{booking.FlightId},{booking.PassengerId},{booking.PassengerName},{booking.SeatClass},{booking.Price.ToString(CultureInfo.InvariantCulture)},{booking.BookingDate:yyyy-MM-dd HH:mm:ss}");
        }
    }
}