using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;
using System.Text;

namespace InventoryManagementSystem.Services;

public class CustomerService : ICustomerService
{
    private List<Customer> customers;
    private readonly string path;

    public CustomerService(string path)
    {
        this.path = path;
        ReadCustomersFromFile();
    }

    public Customer Add(Customer customer)
    {
        var existCustomer = customers.FirstOrDefault(c => c.PhoneNumber.Equals(customer.PhoneNumber));
        if (existCustomer is not null)
            throw new Exception("Customer with this phone number already exists");

        customers.Add(customer);

        WriteCustomersToFile();
        ReadCustomersFromFile();

        return customer;
    }

    public bool Delete(int id)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        var isDeleted = customers.Remove(customer);

        WriteCustomersToFile();
        ReadCustomersFromFile();

        return isDeleted;
    }

    public List<Customer> GetAll()
        => customers;

    public Customer GetById(int id)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        return customer;
    }

    public List<Product> GetPurchaseHistory(int id)
    {
        var customer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        return customer.PurchasedProducts;
    }

    public Customer Update(int id, Customer customer)
    {
        var existCustomer = customers.FirstOrDefault(c => c.Id == id)
            ?? throw new Exception("Customer with this id was not found");

        existCustomer.Id = id;
        existCustomer.LastName = customer.LastName;
        existCustomer.FirstName = customer.FirstName;
        existCustomer.PhoneNumber = customer.PhoneNumber;

        WriteCustomersToFile();
        ReadCustomersFromFile();

        return existCustomer;
    }

    private void WriteCustomersToFile()
    {
        StringBuilder res = new StringBuilder();
        foreach (var customer in customers)
            res.Append($"{customer.Id},{customer.FirstName},{customer.LastName},{customer.PhoneNumber},{ProductListToString(customer.PurchasedProducts)}");

        StreamWriter sw = new StreamWriter(path);
        sw.Write(res);
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
        foreach(var row in arrayData)
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

    private void ReadCustomersFromFile()
    {
        FileStream sm = File.Open(path, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(sm);
        var data = sr.ReadToEnd().Split("\n");
        var res = new List<Customer>();
        sm.Close();
        sr.Close();
        foreach (var row in data)
        {
            if (row == string.Empty)
                continue;
            var saleData = row.Split(',');
            var customer = new Customer() 
            { 
                Id = int.Parse(saleData[0]),
                FirstName = saleData[1],
                LastName = saleData[2],
                PhoneNumber = saleData[3],
                PurchasedProducts = StringToProductList(saleData[4])
            };
            res.Add(customer);
        }
        customers = res;
    }

    public void ReLoad()
    {
        WriteCustomersToFile();
        ReadCustomersFromFile();
    }
}