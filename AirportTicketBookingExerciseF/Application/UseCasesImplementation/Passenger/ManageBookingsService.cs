using AirportTicketBookingExerciseF.Application.Utilities;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.Enums;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;

public class ManageBookingsService : IManageBookingsService
{
    private readonly CsvManageBookingsRepository _manageBookingsRepository;

    public ManageBookingsService(CsvManageBookingsRepository manageBookingsRepository)
    {
        _manageBookingsRepository = manageBookingsRepository;
    }

    public void CancelABooking()
    {
        var allBookings = _manageBookingsRepository.GetAllBookings();

        Console.WriteLine(Messages.EnterPassengerId);
        var passengerId = int.Parse(Console.ReadLine());
        var specificPassengerBookings = SearchBookingsByPassengerId(allBookings, passengerId);
        DeleteBooking(specificPassengerBookings, allBookings);
    }

    public void ModifyABooking()
    {
        var allBookings = _manageBookingsRepository.GetAllBookings();

        Console.WriteLine(Messages.EnterPassengerId);
        var passengerId = int.Parse(Console.ReadLine());
        var specificPassengerBookings = SearchBookingsByPassengerId(allBookings, passengerId);

        if (specificPassengerBookings.Count == 0) return;

        var booking = FindSpecificBooking(specificPassengerBookings);


        Console.WriteLine(Messages.EnterNewSeatClass);
        booking.SeatClass = (SeatClass)Enum.Parse(typeof(SeatClass), Console.ReadLine(), true);

        Console.WriteLine(Messages.EnterNewBookingDate);
        booking.BookingDate = DateTime.Parse(Console.ReadLine());

        var index = allBookings.FindIndex(b => b.BookingId == booking.BookingId);
        if (index != -1) allBookings[index] = booking;

        _manageBookingsRepository.SaveBookings(allBookings);
    }

    public void ViewPersonalBookings()
    {
        var allBookings = _manageBookingsRepository.GetAllBookings();

        Console.WriteLine(Messages.EnterPassengerId);
        var passengerId = int.Parse(Console.ReadLine());
        SearchBookingsByPassengerId(allBookings, passengerId);
    }

    private List<Booking> SearchBookingsByPassengerId(List<Booking> allBookings, int passengerId)
    {
        List<Booking> bookings = allBookings.Where(b => b.PassengerId == passengerId).ToList();
        if (!bookings.Any())
        {
            Console.WriteLine(Messages.NoMatchingBookings);
            return new List<Booking>();
        }

        Console.WriteLine($"\n{Messages.YourBookings}");
        foreach (var booking in bookings)
            Console.WriteLine(
                $"Id: {booking.BookingId}, FlightId: {booking.FlightId}, PassengerId: {booking.PassengerId}" +
                $"PassengerName: {booking.PassengerName}, SeatClass: {booking.SeatClass}, " +
                $"Price: {booking.Price}, BookingDate: {booking.BookingDate}");

        return bookings;
    }

    private void DeleteBooking(List<Booking> bookings, List<Booking> allBookings)
    {
        Console.Write($"\n{Messages.EnterBookingIdToCancel}");
        var bookingIdToDelete = int.Parse(Console.ReadLine());

        var bookingToDelete = bookings.FirstOrDefault(b => b.BookingId == bookingIdToDelete);

        if (bookingToDelete == null)
        {
            Console.WriteLine(Messages.BookingNotFound);
        }
        else
        {
            allBookings.Remove(bookingToDelete);
            Console.WriteLine(Messages.BookingCancelledSuccessfully);

            _manageBookingsRepository.SaveBookings(allBookings);
        }
    }

    private Booking FindSpecificBooking(List<Booking> bookings)
    {
        Console.Write($"\n{Messages.EnterBookingId}");
        var bookingId = int.Parse(Console.ReadLine());

        var booking = bookings.FirstOrDefault(b => b.BookingId == bookingId);

        if (booking == null) Console.WriteLine(Messages.BookingNotFound);

        return booking;
    }
}