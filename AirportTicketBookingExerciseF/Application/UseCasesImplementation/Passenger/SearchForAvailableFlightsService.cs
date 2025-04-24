using AirportTicketBookingExerciseF.Domain.Entities;
using AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;
using AirportTicketBookingExerciseF.Infrastructure.Repositories.Passenger;

namespace AirportTicketBookingExerciseF.Application.UseCasesImplementation.Passenger;

public class SearchForAvailableFlightsService : ISearchForAvailableFlightsService
{
    private readonly ICsvSearchForAvailableFlightsRepository _searchForAvailableFlightsRepository;

    public SearchForAvailableFlightsService(ICsvSearchForAvailableFlightsRepository searchForAvailableFlightsRepository)
    {
        _searchForAvailableFlightsRepository = searchForAvailableFlightsRepository;
    }

    public List<Flight> SearchForAvailableFlights(string? parameter, string? value)
    {
        var flights = _searchForAvailableFlightsRepository.SearchForAvailableFlights();
        var searchResults = flights
            .Where(f => f.GetType().GetProperty(parameter)?.GetValue(f)?.ToString().Contains(value) == true).ToList();

        return searchResults;
    }
}