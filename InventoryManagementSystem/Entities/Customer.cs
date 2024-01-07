namespace InventoryManagementSystem.Entities;

public class Customer
{
    private static int id = 0;

    public Customer()
    {
        this.Id = ++id;
        this.PurchasedProducts = new List<Product>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public List<Product> PurchasedProducts { get; set;}
}
