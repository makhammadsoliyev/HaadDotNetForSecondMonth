namespace InventoryManagementSystem.Entities;

public class Supplier
{
    private static int id = 0;

    public Supplier()
    {
        this.Id = ++id;
        this.Products = new List<Product>();
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public List<Product> Products { get; set; }
}
