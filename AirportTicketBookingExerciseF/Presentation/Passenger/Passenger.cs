using System.Globalization;
using AirportTicketBookingExercise.Application.UseCasesImplementation.Passenger;
using AirportTicketBookingExercise.Domain.Entities;
using AirportTicketBookingExercise.Domain.Enums;
using AirportTicketBookingExercise.Infrastructure.Repositories;
using AirportTicketBookingExercise.Infrastructure.Utilities;

namespace AirportTicketBookingExercise.Presentation.Passenger;

public class Passenger
{

    private Flight? SelectFlightToBook(List<Flight> flights, int flightId)
    {
        var flight = flights.SingleOrDefault(f => f.FlightId == flightId);
        return flight;
    }
    private decimal? GetMaximumLineBookingIds()
    {
        string bookingFlightsFilePath = Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Bookings.csv");
        int columnIndex = 0; 

        try
        {
            var lines = File.ReadAllLines(bookingFlightsFilePath);

            var maxNumber = lines
                .Skip(1) 
                .Select(line => line.Split(',')[columnIndex]) 
                .Select(value => decimal.TryParse(value, out var num) ? num : (decimal?)null) 
                .Where(num => num.HasValue)
                .Max(); 

            Console.WriteLine($"The maximum value in column {columnIndex} is: {maxNumber}");
            return maxNumber;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return null;
    }
    
    
    private void BookAFlight(SearchForAvailableFlightsUseCase searchForAvailableFlightsUseCase, BookAFlightUseCase bookAFlightUseCase)
    {
        var searchResult = SearchForAvailableFlights(searchForAvailableFlightsUseCase);
        if (!searchResult.Any()) Console.WriteLine("No matching flights found.");
        else
        {
            searchResult.ForEach(f => Console.WriteLine($"FlightID: {f.FlightId}, DepartureCountry: {f.DestinationCountry}, DestinationCountry: {f.DestinationCountry}, DepartureDate: {f.DepartureDate}, DepartureAirport: {f.DepartureAirport}, ArrivalAirport: {f.ArrivalAirport}, Price: {f.Price}"));
            
            Console.Write("Enter Flight ID to book flight: ");
            
            if (int.TryParse(Console.ReadLine(), out int flightId))
            {
                if (!searchResult.Any(f => f.FlightId == flightId))
                {
                    Console.WriteLine("Invalid choice. Flight ID not found.");
                }

                var flight = SelectFlightToBook(searchResult, flightId);
                Console.WriteLine("""
                                  Select Seat class: 
                                    1- FirstClass
                                    2- EconomyClass
                                    3- BusinessClass
                                  """);
                string? @class = "";
                int? choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    @class = "FirstClass"; 
                } else if (choice == 2)
                {
                    @class = "Economy";
                } else if (choice == 3)
                {
                    @class = "Business";
                }
                else
                {
                    @class = "Invalid Option";
                }
        
                string? passengerName = Console.ReadLine();

                var booking = new Booking()
                {
                    BookingId = (int)GetMaximumLineBookingIds() + 1,
                    FlightId = flightId,
                    PassengerId = 1, 
                    PassengerName = passengerName,
                    SeatClass = (SeatClass)Enum.Parse(typeof(SeatClass), @class),
                    Price = flight.Price,
                    BookingDate = DateTime.Now
                };
        
                string bookingFlightsFilePath = Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Bookings.csv");
                
                bookAFlightUseCase.BookAFlight(bookingFlightsFilePath, booking);
                Console.WriteLine("Booking added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid numeric Flight ID.");
            }
            
            
        }

        
        
    }
    
    private List<Flight> SearchForAvailableFlights(SearchForAvailableFlightsUseCase searchForAvailableFlightsUseCase)
    {
        Console.Write("""
                      Search by
                        * Departure Country ( DepartureCountry )
                        * Destination Country ( DestinationCountry )
                        * Price (Price)
                        * Departure Date ( DepartureDate )
                        * Departure Airport ( DepartureAirport )
                        * Arrival Airport ( ArrivalAirport )
                      """);
        Console.WriteLine();
        string parameter = Console.ReadLine();
        Console.Write("Enter value: ");
        string value = Console.ReadLine();
        
        var searchResult = searchForAvailableFlightsUseCase.SearchForAvailableFlights(parameter, value);
        
        if (!searchResult.Any()) Console.WriteLine("No matching flights found.");
        else searchResult.ForEach(f => Console.WriteLine($"FlightID: {f.FlightId}, DepartureCountry: {f.DestinationCountry}, DestinationCountry: {f.DestinationCountry}, DepartureDate: {f.DepartureDate}, DepartureAirport: {f.DepartureAirport}, ArrivalAirport: {f.ArrivalAirport}, Price: {f.Price}"));
            
        return searchResult;
    }


    private void CancelABooking(ManageBookingsUseCase  manageBookingUseCase)
    {
        manageBookingUseCase.CancelABooking();
    }

    private void ModifyABooking(ManageBookingsUseCase manageBookingsUseCase)
    {
        manageBookingsUseCase.ModifyABooking();
    }

    private void ViewPersonalBookings(ManageBookingsUseCase manageBookingUseCase)
    {
        manageBookingUseCase.ViewPersonalBookings();
    }

    
    public void Run()
    {   
        string flightsFilePath = Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Flights.csv");
        string bookingsFilePath = Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Bookings.csv");
        var searchForAvailableFlightsParser = new SearchForAvailableFlightsParser();
        var flightBookingsParser = new FlightBookingsParser();
        
        var searchForAvailableFlightsRepository = new CsvSearchForAvailableFlightsRepository(flightsFilePath, searchForAvailableFlightsParser);
        var manageBookingRepository = new CsvManageBookingsRepository(bookingsFilePath, flightBookingsParser);
        var bookAFlightRepository = new CsvBookAFlightRepository(bookingsFilePath, flightBookingsParser);
        
        var searchForAvailableFlightsUseCase = new SearchForAvailableFlightsUseCase(searchForAvailableFlightsRepository);
        var bookAFlightUseCase = new BookAFlightUseCase(bookAFlightRepository);
        var manageBookingsUseCase = new ManageBookingsUseCase(manageBookingRepository);

        
        Menu(searchForAvailableFlightsUseCase, bookAFlightUseCase, manageBookingsUseCase);
    }

    private void Menu(SearchForAvailableFlightsUseCase searchForAvailableFlightsUseCase, BookAFlightUseCase bookAFlightUseCase, ManageBookingsUseCase manageBookingsUseCase)
    {
        while (true)
        {
            Console.WriteLine("\n1. Book a flight\n2. Search for available flights\n3. Manage Bookings\n4. Exit");
            var passengerOperationsChoice = Console.ReadLine();

            switch (passengerOperationsChoice)
            {
                case "1":
                    BookAFlight(searchForAvailableFlightsUseCase,bookAFlightUseCase);
                    break;
                case "2":
                    SearchForAvailableFlights(searchForAvailableFlightsUseCase);
                    break;
                case "3":
                    SubMenu(manageBookingsUseCase);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
    
    private void SubMenu(ManageBookingsUseCase manageBookingsUseCase)
    {
        while (true)
        {
            Console.WriteLine("\n1. Cancel a booking\n2. Modify a booking\n3. View personal bookings\n4. Exit");
            var manageBookingsChoice = Console.ReadLine();

            switch (manageBookingsChoice)
            {
                case "1":
                    CancelABooking(manageBookingsUseCase);
                    break;
                case "2":
                    ModifyABooking(manageBookingsUseCase);
                    break;
                case "3":
                    ViewPersonalBookings(manageBookingsUseCase);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
    }
}

