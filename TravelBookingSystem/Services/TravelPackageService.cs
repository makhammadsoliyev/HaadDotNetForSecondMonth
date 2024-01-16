using System.Text;
using TravelBookingSystem.Configurations;
using TravelBookingSystem.Interfaces;
using TravelBookingSystem.Models;

namespace TravelBookingSystem.Services;

public class TravelPackageService : ITravelPackageService
{
    public async Task<TravelPackage> Add(TravelPackage travelPackage)
    {
        File.AppendAllText(Constants.TRAVEL_PACKAGES_PATH, $"{travelPackage.Id}|{travelPackage.Destination}|{travelPackage.Duration}|{travelPackage.Price}|{travelPackage.Spots}|{travelPackage.Itinerary}\n");
        return travelPackage;
    }
    public async Task<bool> Delete(int id)
    {
        var packages = await GetAll();
        var package = packages.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Package with this id was not found");

        StringBuilder sb = new StringBuilder();
        foreach (var p in packages)
        {
            if (p.Id == id)
                continue;

            sb.AppendLine($"{p.Id}|{p.Destination}|{p.Duration}|{p.Price}|{p.Spots}|{p.Itinerary}");
        }

        File.WriteAllText(Constants.TRAVEL_PACKAGES_PATH, sb.ToString());

        return true;
    }

    public async Task<List<TravelPackage>> GetAll()
    {
        var data = File.ReadAllLines(Constants.TRAVEL_PACKAGES_PATH);
        var packages = new List<TravelPackage>();

        foreach (var line in data)
        {
            var packageData = line.Split('|');
            var package = new TravelPackage()
            {
                Id = Convert.ToInt32(packageData[0]),
                Name = packageData[1],
                Destination = packageData[2],
                Duration = Convert.ToInt32(packageData[3]),
                Price = Convert.ToDecimal(packageData[4]),
                Spots = Convert.ToInt32(packageData[5]),
                Itinerary = packageData[6]
            };

            packages.Add(package);
        }

        return packages;
    }

    public async Task<TravelPackage> GetById(int id)
    {
        var packages = await GetAll();
        var package = packages.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Package with this id was not found");

        return package;
    }

    public async Task<TravelPackage> Update(int id, TravelPackage travelPackage)
    {
        var packages = await GetAll();
        var package = packages.FirstOrDefault(p => p.Id == id)
            ?? throw new Exception("Package with this id was not found");

        package.Id = id;
        package.Name = travelPackage.Name;
        package.Price = travelPackage.Price;
        package.Spots = travelPackage.Spots;
        package.Duration = travelPackage.Duration;
        package.Itinerary = travelPackage.Itinerary;
        package.Destination = travelPackage.Destination;

        StringBuilder sb = new StringBuilder();
        foreach (var p in packages)
            sb.AppendLine($"{p.Id}|{p.Destination}|{p.Duration}|{p.Price}|{p.Spots}|{p.Itinerary}");

        File.WriteAllText(Constants.TRAVEL_PACKAGES_PATH, sb.ToString());

        return package;
    }
}
