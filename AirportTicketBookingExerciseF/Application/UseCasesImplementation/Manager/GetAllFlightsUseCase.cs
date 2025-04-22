using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Manager;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Manager;

public class GetAllFlightsUseCase :  IGetAllFlightsUseCase
{
    private readonly CsvGetAllFlightsRepository _getAllFlightsRepository;

    public GetAllFlightsUseCase(CsvGetAllFlightsRepository getAllFlightsRepository)
    {
        _getAllFlightsRepository = getAllFlightsRepository;
    }
    
    public void GetAllFlights()
    {
        var allFlights = _getAllFlightsRepository.GetAllFlights();
        allFlights.ForEach(f => Console.WriteLine($"FlightID: {f.FlightId}, DepartureCountry: {f.DepartureCountry}, DestinationCountry: {f.DestinationCountry}, DepartureDate: {f.DepartureDate}, DepartureAirport: {f.DepartureAirport}, ArrivalAirport: {f.ArrivalAirport}, Price: {f.Price}"));
    }
}