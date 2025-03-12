using AirportTicketBookingExercise.Domain.UseCasesDeclaration;
using AirportTicketBookingExercise.Infrastructure.Repositories;

namespace AirportTicketBookingExercise.Data.UseCasesImplementation.Manager;

public class GetAllFlightsUseCase :  IGetAllFlightsUseCase
{
    public readonly CsvGetAllFlightsRepository _getAllFlightsRepository;

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