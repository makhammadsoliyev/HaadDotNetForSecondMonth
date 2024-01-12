using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;
using System.Text;

namespace InventoryManagementSystem.Services;

public class SupplierService : ISupplierService
{
    private List<Supplier> suppliers;
    private readonly string path;

    public SupplierService(string path)
    {
        this.path = path;
        ReadSuppliersFromFile();
    }

    public Supplier Add(Supplier supplier)
    {
        var existSupplier = suppliers.FirstOrDefault(s => s.PhoneNumber.Equals(supplier.PhoneNumber));
        if (existSupplier is not null)
            throw new Exception("Supplier with this phone number already exists");

        suppliers.Add(supplier);

        WriteSuppliersToFile();
        ReadSuppliersFromFile();

        return supplier;
    }

    public bool Delete(int id)
    {
        var supplier = suppliers.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Supplier with this id was not found");

        var isDeleted = suppliers.Remove(supplier);

        WriteSuppliersToFile();
        ReadSuppliersFromFile();

        return isDeleted;
    }

    public List<Supplier> GetAll()
        => suppliers;

    public List<Product> GetAllSuppliedProducts(int id)
    {
        var supplier = suppliers.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Supplier with this id was not found");

        return supplier.Products;
    }

    public Supplier GetById(int id)
    {
        var supplier = suppliers.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Supplier with this id was not found");

        return supplier;
    }

    public Supplier Update(int id, Supplier supplier)
    {
        var existSupplier = suppliers.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Supplier with this id was not found");

        existSupplier.Id = id;
        existSupplier.LastName = supplier.LastName;
        existSupplier.FirstName = supplier.FirstName;
        existSupplier.PhoneNumber = supplier.PhoneNumber;

        WriteSuppliersToFile();
        ReadSuppliersFromFile();

        return existSupplier;
    }

    private void ReadSuppliersFromFile()
    {
        List<Supplier> readSuppliers = new List<Supplier>();
        FileStream sm = File.Open(path, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(sm);
        var data = sr.ReadToEnd().Split("\n");
        sm.Close();
        sr.Close();
        foreach (var row in data)
        {
            if (row == string.Empty)
                continue;
            var supplierData = row.Split(',');
            var supplier = new Supplier()
            {
                LastName = supplierData[2],
                FirstName = supplierData[1],
                PhoneNumber = supplierData[3],
                Id = int.Parse(supplierData[0]),
                Products = StringToProductList(supplierData[4])
            };
            readSuppliers.Add(supplier);
        }
        suppliers = readSuppliers;
    }

    private void WriteSuppliersToFile()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var supplier in suppliers)
        {
            sb.AppendLine($"{supplier.Id},{supplier.FirstName},{supplier.LastName},{supplier.PhoneNumber},{ProductListToString(supplier.Products)}");
        }

        StreamWriter sw = new StreamWriter(path);
        sw.Write(sb);
        sw.Close();
    }

    private string ProductListToString(List<Product> products)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var product in products)
            sb.Append($"|{product.Id};{product.Name};{product.Description};{product.Price};{product.StockQuantity};{product.SupplierId}");

        var res = sb.ToString();
        if (res.Length > 0)
            res = res.Substring(1, res.Length - 1);

        return res;
    }

    private List<Product> StringToProductList(string data)
    {
        var res = new List<Product>();
        var arrayData = data.Split("|");
        foreach (var row in arrayData)
        {
            if (row == string.Empty)
                continue;
            var productData = row.Split(";");
            var product = new Product()
            {
                Name = productData[1],
                Description = productData[2],
                Id = int.Parse(productData[0]),
                Price = decimal.Parse(productData[3]),
                SupplierId = int.Parse(productData[5]),
                StockQuantity = int.Parse(productData[4])
            };
            res.Add(product);
        }

        return res;
    }

    public void ReLoad()
    {
        WriteSuppliersToFile();
        ReadSuppliersFromFile();
    }
}
