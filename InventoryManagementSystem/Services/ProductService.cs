using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;

namespace InventoryManagementSystem.Services;

public class ProductService : IProductService
{
    private readonly List<Product> products;
    private readonly SupplierService supplierService;

    public ProductService(SupplierService supplierService)
    {
        this.products = new List<Product>();
        this.supplierService = supplierService;
    }

    public Product Add(Product product)
    {
        var supplier = supplierService.GetById(product.SupplierId);

        supplier.Products.Add(product);
        products.Add(product);
        return product;
    }

    public bool Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Product with this id was not found");

        return products.Remove(product);
    }

    public List<Product> GetAll()
        => products;

    public List<Product> GetAllAvailable()
        => products.FindAll(p => p.StockQuantity > 0);

    public List<Product> GetAllSoldOut()
        => products.FindAll(p => p.StockQuantity == 0);

    public Product GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Product with this id was not found");

        return product;
    }

    public Product Update(int id, Product product)
    {
        var existProduct = products.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Product with this id was not found");

        var supplier = supplierService.GetById(product.SupplierId);
        supplier.Products.Add(existProduct);

        var preSupplier = supplierService.GetById(existProduct.SupplierId);
        preSupplier.Products.Remove(existProduct);

        existProduct.Id = id;
        existProduct.Name = product.Name;
        existProduct.Price = product.Price;
        existProduct.SupplierId = product.SupplierId;
        existProduct.StockQuantity = product.StockQuantity;

        return existProduct;
    }
}
