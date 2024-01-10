namespace InventoryManagementSystem.Entities;

public class Sale
{
    private static int id = 0;

    public Sale()
    {
        this.Id = ++id;
    }

    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public DateTime SoldDate { get; set; }
}
