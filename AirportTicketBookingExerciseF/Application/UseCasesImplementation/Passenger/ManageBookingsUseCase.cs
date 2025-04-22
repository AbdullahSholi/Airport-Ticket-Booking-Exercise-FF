using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.Enums;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;

public class ManageBookingsUseCase :  IManageBookingsUseCase
{

    private readonly CsvManageBookingsRepository  _manageBookingsRepository;
    public ManageBookingsUseCase(CsvManageBookingsRepository manageBookingsRepository)
    {
        _manageBookingsRepository = manageBookingsRepository;
    }

    private List<Booking> SearchBookingsByPassengerId(List<Booking> allBookings, int passengerId)
    {
        List<Booking> bookings = allBookings.Where(b => b.PassengerId == passengerId).ToList();
        if(!bookings.Any())
        {
            Console.WriteLine("No bookings found for this passenger.");
            return new List<Booking>();
        }
        
        Console.WriteLine("\nYour Bookings:");
        foreach (var booking in bookings)
        {
            Console.WriteLine($"Id: {booking.BookingId}, FlightId: {booking.FlightId}, PassengerId: {booking.PassengerId}" + 
                              $"PassengerName: {booking.PassengerName}, SeatClass: {booking.SeatClass}, " +
                              $"Price: {booking.Price}, BookingDate: {booking.BookingDate}");
        }
        
        return bookings;
        
    }

    private void DeleteBooking(List<Booking> bookings, List<Booking> allBookings)
    {
        Console.Write("\nEnter the booking Id to cancel: ");
        int bookingIdToDelete = int.Parse(Console.ReadLine());

        var bookingToDelete = bookings.FirstOrDefault(b => b.BookingId == bookingIdToDelete);

        if (bookingToDelete == null)
        {
            Console.WriteLine("Booking not found.");
        }
        else
        {
            allBookings.Remove(bookingToDelete);
            Console.WriteLine("Booking cancelled successfully!");

            _manageBookingsRepository.SaveBookings(allBookings);
        }
    }

    private Booking FindSpecificBooking(List<Booking> bookings)
    {
        Console.Write("\nEnter the booking Id: ");
        int bookingId = int.Parse(Console.ReadLine());

        var booking = bookings.FirstOrDefault(b => b.BookingId == bookingId);

        if (booking == null)
        {
            Console.WriteLine("Booking not found.");
        }
        
        return booking;
    }
    
    public void CancelABooking()
    {
        var allBookings = _manageBookingsRepository.GetAllBookings();

        Console.WriteLine("Enter your passenger id: ");
        int passengerId = int.Parse(Console.ReadLine());
        var specificPassengerBookings = SearchBookingsByPassengerId(allBookings, passengerId);
        DeleteBooking(specificPassengerBookings, allBookings);
        
    }

    public void ModifyABooking()
    {
        var allBookings = _manageBookingsRepository.GetAllBookings();

        Console.WriteLine("Enter your passenger id: ");
        int passengerId = int.Parse(Console.ReadLine());
        var specificPassengerBookings = SearchBookingsByPassengerId(allBookings, passengerId);
        
        if (specificPassengerBookings.Count == 0) return;
        
        var booking = FindSpecificBooking(specificPassengerBookings);
        

        Console.WriteLine("Enter new SeatClass: (e.g. FirstClass, Economy, Business)");
        booking.SeatClass = (SeatClass)Enum.Parse(typeof(SeatClass), Console.ReadLine(), true);

        Console.WriteLine("Enter new Date: (e.g. yyyy-MM-dd HH:mm:ss) ");
        booking.BookingDate = DateTime.Parse(Console.ReadLine());
        
        int index = allBookings.FindIndex(b => b.BookingId == booking.BookingId);
        if (index != -1)
        {
            allBookings[index] = booking;
        }
        
        _manageBookingsRepository.SaveBookings(allBookings);
        
    }
    
    

    public void ViewPersonalBookings()
    {
        var allBookings = _manageBookingsRepository.GetAllBookings();

        Console.WriteLine("Enter your passenger id: ");
        int passengerId = int.Parse(Console.ReadLine());
        SearchBookingsByPassengerId(allBookings, passengerId);
    }
}