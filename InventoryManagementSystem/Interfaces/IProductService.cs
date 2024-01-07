using InventoryManagementSystem.Entities;

namespace InventoryManagementSystem.Interfaces;

public interface IProductService
{
    Product Add(Product product);
    Product GetById(int id);
    Product Update(int id, Product product);
    bool Delete(int id);
    List<Product> GetAll();
}
