namespace InventoryManagementSystem.Entities;

public class Product
{
    private static int id = 0;

    public Product()
    {
        this.Id = ++id;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int SupplierId { get; set; }
}
