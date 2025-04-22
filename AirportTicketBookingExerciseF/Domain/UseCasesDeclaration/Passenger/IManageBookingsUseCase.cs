namespace AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

public interface IManageBookingsUseCase
{
    public void CancelABooking();
    public void ModifyABooking();
    public void ViewPersonalBookings();
}