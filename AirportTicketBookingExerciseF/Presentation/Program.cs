namespace AirportTicketBookingExerciseF.Presentation
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            Manager.Manager manager = new Manager.Manager();
            Passenger.Passenger passenger = new Passenger.Passenger();
            Menu(manager, passenger);
        }
        
        public static void Menu(Manager.Manager manager, Passenger.Passenger passenger)
        {
            Console.WriteLine("Welcome to Product Management!");
            while (true)
            {
                Console.WriteLine("\n1. Passenger\n2. Manager\n3. Exit");
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
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }
    }
}



