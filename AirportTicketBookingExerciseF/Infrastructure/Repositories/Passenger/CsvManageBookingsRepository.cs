using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Infrastructure.Utilities;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Manager;

public class CsvManageBookingsRepository
{
    private readonly string? _csvFilePath;
    private readonly FlightBookingsParser _flightBookingsParser;

    public CsvManageBookingsRepository(string? csvFilePath, FlightBookingsParser flightBookingsParser)
    {
        _csvFilePath = csvFilePath;
        _flightBookingsParser = flightBookingsParser;
    }

    public void SaveBookings(List<Booking> bookings)
    {
        var lines = new List<string>
        {
            Messages.CsvBookingHeader
        };

        lines.AddRange(bookings.Select(b =>
            $"{b.BookingId},{b.FlightId},{b.PassengerId},{b.PassengerName},{b.SeatClass},{b.Price},{b.BookingDate:yyyy-MM-dd HH:mm:ss}"));

        File.WriteAllLines(_csvFilePath, lines);
    }

    public List<Booking> GetAllBookings()
    {
        if (!File.Exists(_csvFilePath)) return new List<Booking>();

        var lines = File.ReadAllLines(_csvFilePath).Skip(1);
        return lines.Select(line => _flightBookingsParser.ParseBooking(line)).ToList();
    }
}