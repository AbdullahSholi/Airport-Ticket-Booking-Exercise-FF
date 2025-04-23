namespace AirportTicketBookingExerciseF.Presentation.Utilities;

public class Messages
{
    public const string WelcomeToProgram = "Welcome to Product Management!";
    public const string UsersMenu = "\n1. Passenger\n2. Manager\n3. Exit";
    public const string InvalidChoice = "Invalid choice, please try again.";

    public const string FilterBookingParameters = """
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
                                                  """;

    public const string EnterValue = "Enter value";
    public const string ManagerMenu = "\n1. Filter Bookings\n2. Get all flights from CSV\n3. Exit";

    public const string PassengerMenu =
        "\n1. Book a flight\n2. Search for available flights\n3. Manage Bookings\n4. Exit";

    public const string PassengerManageBookingsMenu =
        "\n1. Cancel a booking\n2. Modify a booking\n3. View personal bookings\n4. Exit";

    public const string NoMatchingFlight = "No matching flights found.";
    public const string EnterFlightBooking = "Enter Flight ID to book flight: ";
    public const string FlightIdNotFound = "Flight ID not found.";

    public const string SeatClassMenu = """
                                        Select Seat class: 
                                          1- FirstClass
                                          2- EconomyClass
                                          3- BusinessClass
                                        """;

    public const string FirstClass = "FirstClass";
    public const string EconomyClass = "Economy";
    public const string BusinessClass = "Business";
    public const string InvalidOption = "Invalid Option";
    public const string BookingSuccessfully = "Booking added successfully!";
    public const string InvalidFlightId = "Invalid Flight ID";

    public const string FilterAvailableFlightParameters = """
                                                          Search by
                                                            * Departure Country ( DepartureCountry )
                                                            * Destination Country ( DestinationCountry )
                                                            * Price (Price)
                                                            * Departure Date ( DepartureDate )
                                                            * Departure Airport ( DepartureAirport )
                                                            * Arrival Airport ( ArrivalAirport )
                                                          """;
}