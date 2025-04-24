using AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;
using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.Enums;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Manager;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Passenger;
using AirportTicketBookingExerciseF.Presentation.Utilities;

namespace AirportTicketBookingExerciseF.Presentation.Passenger;

public class Passenger
{
    public void Run()
    {
        var flightsFilePath =
            Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Flights.csv");
        var bookingsFilePath =
            Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Bookings.csv");
        var searchForAvailableFlightsParser = new SearchForAvailableFlightsParser();
        var flightBookingsParser = new FlightBookingsParser();

        var searchForAvailableFlightsRepository =
            new CsvSearchForAvailableFlightsRepository(flightsFilePath, searchForAvailableFlightsParser);
        var manageBookingRepository = new CsvManageBookingsRepository(bookingsFilePath, flightBookingsParser);
        var bookAFlightRepository = new CsvBookAFlightRepository(bookingsFilePath, flightBookingsParser);

        var searchForAvailableFlightsService =
            new SearchForAvailableFlightsService(searchForAvailableFlightsRepository);
        var bookAFlightService = new BookAFlightService(bookAFlightRepository);
        var manageBookingsService = new ManageBookingsService(manageBookingRepository);


        Menu(searchForAvailableFlightsService, bookAFlightService, manageBookingsService);
    }

    private void Menu(ISearchForAvailableFlightsService searchForAvailableFlightsService,
        IBookAFlightService bookAFlightService, IManageBookingsService manageBookingsService)
    {
        while (true)
        {
            Console.WriteLine(Messages.PassengerMenu);
            var passengerOperationsChoice = Console.ReadLine();

            switch (passengerOperationsChoice)
            {
                case "1":
                    BookAFlight(searchForAvailableFlightsService, bookAFlightService);
                    break;
                case "2":
                    SearchForAvailableFlights(searchForAvailableFlightsService);
                    break;
                case "3":
                    SubMenu(manageBookingsService);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine(Messages.InvalidChoice);
                    break;
            }
        }
    }

    private void SubMenu(IManageBookingsService manageBookingsService)
    {
        while (true)
        {
            Console.WriteLine(Messages.PassengerManageBookingsMenu);
            var manageBookingsChoice = Console.ReadLine();

            switch (manageBookingsChoice)
            {
                case "1":
                    CancelABooking(manageBookingsService);
                    break;
                case "2":
                    ModifyABooking(manageBookingsService);
                    break;
                case "3":
                    ViewPersonalBookings(manageBookingsService);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine(Messages.InvalidChoice);
                    break;
            }
        }
    }

    private Flight? SelectFlightToBook(List<Flight> flights, int flightId)
    {
        var flight = flights.SingleOrDefault(f => f.FlightId == flightId);
        return flight;
    }

    private decimal? GetMaximumLineBookingIds()
    {
        var bookingFlightsFilePath =
            Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Bookings.csv");
        var columnIndex = 0;

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

    private void BookAFlight(ISearchForAvailableFlightsService searchForAvailableFlightsService,
        IBookAFlightService bookAFlightService)
    {
        var searchResult = SearchForAvailableFlights(searchForAvailableFlightsService);
        if (!searchResult.Any())
        {
            Console.WriteLine(Messages.NoMatchingFlight);
        }
        else
        {
            searchResult.ForEach(f =>
                Console.WriteLine(
                    $"FlightID: {f.FlightId}, DepartureCountry: {f.DestinationCountry}, DestinationCountry: {f.DestinationCountry}, DepartureDate: {f.DepartureDate}, DepartureAirport: {f.DepartureAirport}, ArrivalAirport: {f.ArrivalAirport}, Price: {f.Price}"));

            Console.Write(Messages.EnterFlightBooking);

            if (int.TryParse(Console.ReadLine(), out var flightId))
            {
                if (!searchResult.Any(f => f.FlightId == flightId))
                    Console.WriteLine(Messages.FlightIdNotFound);

                var flight = SelectFlightToBook(searchResult, flightId);
                Console.WriteLine("""
                                  Select Seat class: 
                                    1- FirstClass
                                    2- EconomyClass
                                    3- BusinessClass
                                  """);
                var @class = "";
                int? choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                    @class = Messages.FirstClass;
                else if (choice == 2)
                    @class = Messages.EconomyClass;
                else if (choice == 3)
                    @class = Messages.BusinessClass;
                else
                    @class = Messages.InvalidOption;

                var passengerName = Console.ReadLine();

                var booking = new Booking
                {
                    BookingId = (int)GetMaximumLineBookingIds() + 1,
                    FlightId = flightId,
                    PassengerId = 1,
                    PassengerName = passengerName,
                    SeatClass = (SeatClass)Enum.Parse(typeof(SeatClass), @class),
                    Price = flight.Price,
                    BookingDate = DateTime.Now
                };

                var bookingFlightsFilePath = Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData",
                    "Bookings.csv");

                bookAFlightService.BookAFlight(bookingFlightsFilePath, booking);
                Console.WriteLine(Messages.BookingSuccessfully);
            }
            else
            {
                Console.WriteLine(Messages.InvalidFlightId);
            }
        }
    }

    private List<Flight> SearchForAvailableFlights(ISearchForAvailableFlightsService searchForAvailableFlightsService)
    {
        Console.Write(Messages.FilterAvailableFlightParameters);
        Console.WriteLine();
        var parameter = Console.ReadLine();
        Console.Write(Messages.EnterValue);
        var value = Console.ReadLine();

        var searchResult = searchForAvailableFlightsService.SearchForAvailableFlights(parameter, value);

        if (!searchResult.Any()) Console.WriteLine(Messages.NoMatchingFlight);
        else
            searchResult.ForEach(f =>
                Console.WriteLine(
                    $"FlightID: {f.FlightId}, DepartureCountry: {f.DestinationCountry}, DestinationCountry: {f.DestinationCountry}, DepartureDate: {f.DepartureDate}, DepartureAirport: {f.DepartureAirport}, ArrivalAirport: {f.ArrivalAirport}, Price: {f.Price}"));

        return searchResult;
    }

    private void CancelABooking(IManageBookingsService manageBookingService)
    {
        manageBookingService.CancelABooking();
    }

    private void ModifyABooking(IManageBookingsService manageBookingsService)
    {
        manageBookingsService.ModifyABooking();
    }

    private void ViewPersonalBookings(IManageBookingsService manageBookingService)
    {
        manageBookingService.ViewPersonalBookings();
    }
}