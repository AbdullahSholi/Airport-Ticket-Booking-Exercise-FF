namespace AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Manager;

public interface IFilterBookingsService
{
    public void FilterBookings(string? parameter, string? value);
}