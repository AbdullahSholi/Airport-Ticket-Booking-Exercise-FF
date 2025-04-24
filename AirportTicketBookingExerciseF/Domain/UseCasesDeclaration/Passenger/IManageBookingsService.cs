namespace AirportTicketBookingExerciseF.Domain.UseCasesDeclaration.Passenger;

public interface IManageBookingsService
{
    public void CancelABooking();
    public void ModifyABooking();
    public void ViewPersonalBookings();
}