namespace TravelBookingSystem.Models;

public class TravelPackage : Auditable
{
    private static int id = 0;

    public TravelPackage()
    {
        Id = ++id;
    }

    public string Name { get; set; }
    public string Destination { get; set; }
    public int Duration { get; set; }
    public decimal Price { get; set; }
    public int Spots { get; set; }
    public string Itinerary { get; set; }
}
