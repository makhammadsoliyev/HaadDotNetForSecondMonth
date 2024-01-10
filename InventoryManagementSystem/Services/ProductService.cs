using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;
using System.Text;

namespace InventoryManagementSystem.Services;

public class ProductService : IProductService
{
    private List<Product> products;
    private readonly SupplierService supplierService;
    private readonly string path;
    public ProductService(SupplierService supplierService, string path)
    {
        this.path = path;
        this.supplierService = supplierService;
        ReadProductsFromFile();    
    }

    public Product Add(Product product)
    {
        var supplier = supplierService.GetById(product.SupplierId);

        supplier.Products.Add(product);
        products.Add(product);

        WriteProductsToFile();
        ReadProductsFromFile();

        supplierService.ReLoad();

        return product;
    }

    public bool Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Product with this id was not found");

        var isDeleted = products.Remove(product);

        WriteProductsToFile();
        ReadProductsFromFile();

        supplierService.ReLoad();

        return isDeleted;
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

        WriteProductsToFile();
        ReadProductsFromFile();

        supplierService.ReLoad();

        return existProduct;
    }

    private void WriteProductsToFile()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var product in products)
            sb.AppendLine($"{product.Id},{product.Name},{product.Description},{product.Price},{product.StockQuantity},{product.SupplierId}");

        StreamWriter sw = new StreamWriter(path);
        sw.Write(sb);
        sw.Close();
    }

    private void ReadProductsFromFile()
    {
        List<Product> readProducts = new List<Product>();
        FileStream sm = File.Open(path, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(sm);
        var data = sr.ReadToEnd().Split("\n");
        sr.Close();
        foreach(var row in data)
        {
            if (row == string.Empty)
                continue;
            var productData = row.Split(",");
            var product = new Product()
            {
                Name = productData[1],
                Description = productData[2],
                Id = int.Parse(productData[0]),
                Price = decimal.Parse(productData[3]),
                SupplierId = int.Parse(productData[5]),
                StockQuantity = int.Parse(productData[4])
            };
            readProducts.Add(product);
        }
        products = readProducts;
    }
}
