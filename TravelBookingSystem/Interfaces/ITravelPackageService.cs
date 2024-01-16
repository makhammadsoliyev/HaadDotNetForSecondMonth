using TravelBookingSystem.Models;

namespace TravelBookingSystem.Interfaces;

public interface ITravelPackageService
{
    Task<TravelPackage> Add(TravelPackage travelPackage);
    Task<TravelPackage> GetById(int id);
    Task<TravelPackage> Update(int id, TravelPackage travelPackage);
    Task<bool> Delete(int id);
    Task<List<TravelPackage>> GetAll();
    Task<List<TravelPackage>> SearchByPackageName(string name);
}
