namespace AirportTicketBookingExercise.Domain.UseCasesDeclaration;

public interface IFilterBookingsUseCase
{
    public void FilterBookings(string? parameter, string? value);

}