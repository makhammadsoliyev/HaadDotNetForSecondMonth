using InventoryManagementSystem.Entities;
using InventoryManagementSystem.Interfaces;

namespace InventoryManagementSystem.Services;

public class SupplierService : ISupplierService
{
    private readonly List<Supplier> suppliers;

    public SupplierService()
    {
        this.suppliers = new List<Supplier>();
    }

    public Supplier Add(Supplier supplier)
    {
        var existSupplier = suppliers.FirstOrDefault(s => s.PhoneNumber.Equals(supplier.PhoneNumber));
        if (existSupplier is not null)
            throw new Exception("Supplier with this phone number already exists");

        suppliers.Add(supplier);
        return supplier;
    }

    public bool Delete(int id)
    {
        var supplier = suppliers.FirstOrDefault(s => s.Id == id)
            ?? throw new Exception("Supplier with this id was not found");

        return suppliers.Remove(supplier);
    }

    public List<Supplier> GetAll()
        => suppliers;

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

        return existSupplier;
    }
}
