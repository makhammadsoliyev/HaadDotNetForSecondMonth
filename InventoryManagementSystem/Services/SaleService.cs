using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;
using System.Text;

namespace InventoryManagementSystem.Services;

public class SaleService : ISaleService
{
    private List<Sale> sales;
    private readonly ProductService productService;
    private readonly CustomerService customerService;
    private readonly string path;

    public SaleService(CustomerService customerService, ProductService productService, string path)
    {
        this.productService = productService;
        this.customerService = customerService;
        this.path = path;
        ReadSalesFromFile();
    }

    public Sale Add(Sale sale)
    {
        var product = productService.GetById(sale.ProductId);
        var customer = customerService.GetById(sale.CustomerId);

        if (product.StockQuantity < sale.Quantity)
            throw new Exception("This amount of product is not available");
        product.StockQuantity -= sale.Quantity;
        customer.PurchasedProducts.Add(product);
        sales.Add(sale);

        WriteSalesToFile();
        ReadSalesFromFile();

        customerService.ReLoad();

        return sale;
    }

    public bool Delete(int id)
    {
        var sale = sales.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Sale with this id was not found");

        var isDeleted = sales.Remove(sale);

        WriteSalesToFile();
        ReadSalesFromFile();

        customerService.ReLoad();

        return isDeleted;
    }

    public List<Sale> GetAll()
        => sales;

    public Sale GetById(int id)
    {
        var sale = sales.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Sale with this id was not found");

        return sale;
    }

    public Sale Update(int id, Sale sale)
    {
        var existSale = sales.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Sale with this id was not found");

        var product = productService.GetById(existSale.ProductId);
        var customer = customerService.GetById(existSale.CustomerId);
        customer.PurchasedProducts.Remove(product);

        var newCustomer = customerService.GetById(sale.CustomerId);
        newCustomer.PurchasedProducts.Add(product);

        if (sale.Quantity > existSale.Quantity + product.StockQuantity)
            throw new Exception("This amount of product is not available");

        product.StockQuantity += existSale.Quantity;

        existSale.Id = id;
        existSale.ProductId = sale.ProductId;
        existSale.CustomerId = sale.CustomerId;
        existSale.Quantity = sale.Quantity;

        WriteSalesToFile();
        ReadSalesFromFile();

        customerService.ReLoad();

        return existSale;
    }

    private void WriteSalesToFile()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var sale in sales)
            sb.AppendLine($"{sale.Id},{sale.ProductId},{sale.CustomerId},{sale.Quantity},{sale.SoldDate}");

        StreamWriter sw = new StreamWriter(path);
        sw.Write(sb);
        sw.Close();
    }

    private void ReadSalesFromFile()
    {
        List<Sale> readSales = new List<Sale>();
        FileStream sm = File.Open(path, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(sm);
        var data = sr.ReadToEnd().Split("\n");
        sr.Close();
        foreach (var row in data)
        {
            if (row == string.Empty)
                continue;
            var saleData = row.Split(',');
            var sale = new Sale()
            {
                Id = int.Parse(saleData[0]),
                Quantity = int.Parse(saleData[3]),
                ProductId = int.Parse(saleData[1]),
                CustomerId = int.Parse(saleData[2]),
                SoldDate = DateTime.Parse(saleData[4])
            };
            readSales.Add(sale);
        }
        sales = readSales;
    }
}
