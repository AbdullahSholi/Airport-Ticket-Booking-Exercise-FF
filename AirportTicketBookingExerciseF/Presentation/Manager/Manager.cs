using AirportTicketBookingExercise.Data.UseCasesImplementation.Manager;
using AirportTicketBookingExercise.Infrastructure.Repositories;
using AirportTicketBookingExercise.Infrastructure.Utilities;
using Microsoft.VisualBasic;

namespace AirportTicketBookingExercise.Presentation.Manager;

public class Manager
{   
    
    public void FilterBookings(FilterBookingsUseCase filterBookingsUseCase)
    {
        Console.Write("""
                      Enter filter parameter
                        * Departure Country ( DepartureCountry )
                        * Destination Country ( DestinationCountry )
                        * Flight ( Flight )
                        * Price ( Price )
                        * Departure Date ( DepartureDate )
                        * Departure Airport ( DepartureAirport )
                        * Arrival Airport ( ArrivalAirport )
                        * Class ( SeatClass )
                        * Passenger ( Passenger )
                      """);
        Console.WriteLine();
        string parameter = Console.ReadLine();
        Console.Write("Enter value: ");
        string value = Console.ReadLine();

        filterBookingsUseCase.FilterBookings(parameter, value);
            
    }
    
    public void GetAllFlights(GetAllFlightsUseCase getAllFlightsUseCase)
    {
        getAllFlightsUseCase.GetAllFlights();
    }
    
    
    public void Run()
    {
        string passnegersFlightsFilePath = Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Passengers.csv");
        string bookingFlightsFilePath = Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Bookings.csv");
        string flightsFilePath = Path.Combine(Constants.Constants.BaseCsvPath, "Infrastructure", "FileData", "Flights.csv");
        var flightBookingsParser = new FlightBookingsParser();
        var getAllFlightsParser = new GetAllFlightsParser();
        var passengersParser = new GetAllPassengersParser();

        
        var flightRepository = new CsvFilterBookingsRepository(bookingFlightsFilePath, passnegersFlightsFilePath, flightsFilePath, passengersParser, flightBookingsParser, getAllFlightsParser);
        var getAllFlightsRepository = new CsvGetAllFlightsRepository(flightsFilePath, getAllFlightsParser);
   
        
        var filterBookingsUseCase = new FilterBookingsUseCase(flightRepository);
        var getAllFlightsUseCase = new GetAllFlightsUseCase(getAllFlightsRepository);
        Menu(filterBookingsUseCase, getAllFlightsUseCase);
    }
    

    public void Menu(FilterBookingsUseCase filterBookingsUseCase, GetAllFlightsUseCase getAllFlightsUseCase)
    {
        while (true)
        {
            Console.WriteLine("\n1. Filter Bookings\n2. Get all flights from CSV\n3. Exit");
            var managerOperationsChoice = Console.ReadLine();

            switch (managerOperationsChoice)
            {
                case "1":
                    FilterBookings(filterBookingsUseCase);
                    break;
                case "2":
                    GetAllFlights(getAllFlightsUseCase);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again.");
                    break;
            }
        }
        
        
    }
}