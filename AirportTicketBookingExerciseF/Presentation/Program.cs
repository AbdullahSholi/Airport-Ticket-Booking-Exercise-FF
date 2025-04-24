using AirportTicketBookingExerciseF.Presentation.Utilities;

namespace AirportTicketBookingExerciseF.Presentation;

public class Program
{
    private static void Main(string[] args)
    {
        var manager = new Manager.Manager();
        var passenger = new Passenger.Passenger();
        Menu(manager, passenger);
    }

    public static void Menu(Manager.Manager manager, Passenger.Passenger passenger)
    {
        Console.WriteLine(Messages.WelcomeToProgram);
        while (true)
        {
            Console.WriteLine(Messages.UsersMenu);
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    passenger.Run();
                    break;
                case "2":
                    manager.Run();
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