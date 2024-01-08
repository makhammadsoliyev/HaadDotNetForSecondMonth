using InventoryManagementSystem.Entities;

namespace InventoryManagementSystem.Interfaces;

public interface ISupplierService
{
    Supplier Add(Supplier supplier);
    Supplier GetById(int id);
    Supplier Update(int id, Supplier supplier);
    bool Delete(int id);
    List<Supplier> GetAll();
    List<Product> GetAllSuppliedProducts(int id);
}
