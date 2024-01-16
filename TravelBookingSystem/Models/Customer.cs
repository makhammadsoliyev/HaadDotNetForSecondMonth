namespace TravelBookingSystem.Models;

public class Customer : Auditable
{
    private static int id = 0;
    public Customer()
    {
        Id = ++id;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}
