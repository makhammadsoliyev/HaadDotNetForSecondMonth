namespace TravelBookingSystem.Models;

public class Booking : Auditable
{
    private static int id = 0;
    public Booking()
    {
        Id = ++id;
    }

    public int PackageId { get; set; }
    public int CustomerId { get; set; }
    public int NumberOfTravelers { get; set; }
    public DateOnly TravelDate { get; set; }
}
