using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;

public class SearchForAvailableFlightsUseCase : ISearchForAvailableFlightsUseCase
{
    private readonly CsvSearchForAvailableFlightsRepository  _searchForAvailableFlightsRepository;

    public SearchForAvailableFlightsUseCase(CsvSearchForAvailableFlightsRepository searchForAvailableFlightsRepository)
    {
        _searchForAvailableFlightsRepository = searchForAvailableFlightsRepository;
    }
    
    public List<Flight> SearchForAvailableFlights(string? parameter, string? value)
    {
        
        var flights = _searchForAvailableFlightsRepository.SearchForAvailableFlights();
        var searchResults = flights.Where(f => f.GetType().GetProperty(parameter)?.GetValue(f)?.ToString().Contains(value) == true).ToList();
        
        return searchResults;
        
    }
}