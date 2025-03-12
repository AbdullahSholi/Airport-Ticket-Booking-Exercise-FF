using AirportTicketBookingExercise.Domain.Entities;
using AirportTicketBookingExercise.Domain.UseCasesDeclaration.Passenger;
using AirportTicketBookingExercise.Infrastructure.Repositories;

namespace AirportTicketBookingExercise.Application.UseCasesImplementation.Passenger;

public class SearchForAvailableFlightsUseCase : ISearchForAvailableFlightsUseCase
{
    public readonly CsvSearchForAvailableFlightsRepository  _searchForAvailableFlightsRepository;

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