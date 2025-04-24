using AirportTicketBookingExerciseF.Application.UseCasesImplementation.Manager;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Manager;
using AirportTicketBookingExerciseF.Infrastructure.Utilities.Manager;
using AirportTicketBookingExerciseF.Presentation.Utilities;

namespace AirportTicketBookingExerciseF.Presentation.Manager;

public class Manager
{
    public void Run()
    {
        var passnegersFlightsFilePath =
            Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Passengers.csv");
        var bookingFlightsFilePath =
            Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Bookings.csv");
        var flightsFilePath =
            Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Flights.csv");
        var flightBookingsParser = new FlightBookingsParser();
        var getAllFlightsParser = new GetAllFlightsParser();
        var passengersParser = new GetAllPassengersParser();


        var flightRepository = new CsvFilterBookingsRepository(bookingFlightsFilePath, passnegersFlightsFilePath,
            flightsFilePath, passengersParser, flightBookingsParser, getAllFlightsParser);
        var getAllFlightsRepository = new CsvGetAllFlightsRepository(flightsFilePath, getAllFlightsParser);


        var filterBookingsService = new FilterBookingsService(flightRepository);
        var getAllFlightsService = new GetAllFlightsService(getAllFlightsRepository);
        Menu(filterBookingsService, getAllFlightsService);
    }

    private void FilterBookings(IFilterBookingsService filterBookingsService)
    {
        Console.Write(Messages.FilterBookingParameters);
        Console.WriteLine();
        var parameter = Console.ReadLine();
        Console.Write(Messages.EnterValue);
        var value = Console.ReadLine();

        filterBookingsService.FilterBookings(parameter, value);
    }

    private void GetAllFlights(IGetAllFlightsService getAllFlightsService)
    {
        getAllFlightsService.GetAllFlights();
    }

    private void Menu(IFilterBookingsService filterBookingsService, IGetAllFlightsService getAllFlightsService)
    {
        while (true)
        {
            Console.WriteLine(Messages.ManagerMenu);
            var managerOperationsChoice = Console.ReadLine();

            switch (managerOperationsChoice)
            {
                case "1":
                    FilterBookings(filterBookingsService);
                    break;
                case "2":
                    GetAllFlights(getAllFlightsService);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine(Messages.InvalidChoice);
                    break;
            }
        }
    }
}