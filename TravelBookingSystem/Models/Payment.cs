namespace TravelBookingSystem.Models;

public class Payment : Auditable
{
    private static int id = 0;
    public Payment()
    {
        Id = ++id;
    }

    public int BookingId { get; set; }
    public PaymentStatus Status { get; set; }
    public Decimal TotalAmount { get; set; }
}

