using InventoryManagementSystem.Entities;

namespace InventoryManagementSystem.Interfaces;

public interface ISaleService
{
    Sale Add(Sale sale);
    Sale GetById(int id);
    Sale Update(int id, Sale sale);
    bool Delete(int id);
    List<Sale> GetAll();
}
