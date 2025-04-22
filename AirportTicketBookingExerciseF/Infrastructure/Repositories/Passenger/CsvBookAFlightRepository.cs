using System.Globalization;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Utilities;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Manager;


public class CsvBookAFlightRepository
{
    public readonly string? _csvFilePath;
    public FlightBookingsParser _flightBookingsParser;

    public CsvBookAFlightRepository(string? csvFilePath,  FlightBookingsParser flightBookingsParser)
    {
        _csvFilePath = csvFilePath;
        _flightBookingsParser = flightBookingsParser;
    }
    
    public void AppendBookingToCsv(string filePath, Booking booking)
    {
        bool fileExists = File.Exists(filePath);
        
        using (StreamWriter writer = new StreamWriter(filePath, true)) 
        {
            if (!fileExists)
            {
                writer.WriteLine("BookingId,FlightId,PassengerId,PassengerName,SeatClass,Price,BookingDate");
            }

            writer.WriteLine($"{booking.BookingId},{booking.FlightId},{booking.PassengerId},{booking.PassengerName},{booking.SeatClass},{booking.Price.ToString(CultureInfo.InvariantCulture)},{booking.BookingDate:yyyy-MM-dd HH:mm:ss}");
        }
    }
}