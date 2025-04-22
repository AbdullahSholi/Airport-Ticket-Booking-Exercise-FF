namespace AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Manager;

public interface IFilterBookingsUseCase
{
    public void FilterBookings(string? parameter, string? value);

}