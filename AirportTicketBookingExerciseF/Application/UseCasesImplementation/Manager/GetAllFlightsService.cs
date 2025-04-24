using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Manager;
using AirportTicketBookingExerciseF.Infrastructure.Repositories.Manager;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Manager;

public class GetAllFlightsService : IGetAllFlightsService
{
    private readonly ICsvGetAllFlightsRepository _getAllFlightsRepository;

    public GetAllFlightsService(ICsvGetAllFlightsRepository getAllFlightsRepository)
    {
        _getAllFlightsRepository = getAllFlightsRepository;
    }

    public void GetAllFlights()
    {
        var allFlights = _getAllFlightsRepository.GetAllFlights();
        allFlights.ForEach(f =>
            Console.WriteLine(
                $"FlightID: {f.FlightId}, DepartureCountry: {f.DepartureCountry}, DestinationCountry: {f.DestinationCountry}, DepartureDate: {f.DepartureDate}, DepartureAirport: {f.DepartureAirport}, ArrivalAirport: {f.ArrivalAirport}, Price: {f.Price}"));
    }
}