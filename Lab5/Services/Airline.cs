using lab5.Interfaces;
using lab5.Models;

namespace lab5.Services;

/// <summary>
/// Авиакомпания, управляющая списком воздушных судов.
/// </summary>
public class Airline : IAircraftSearch
{
    private readonly List<Aircraft> aircrafts = new();

    public string Name { get; }

    public Airline(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название авиакомпании не может быть пустым.", nameof(name));
        }

        Name = name;
    }

    public void AddAircraft(Aircraft aircraft)
    {
        ArgumentNullException.ThrowIfNull(aircraft);
        aircrafts.Add(aircraft);
    }

    public int GetTotalPassengerCapacity()
    {
        return aircrafts.Sum(aircraft => aircraft.GetPassengerCapacity());
    }

    public double GetTotalCargoCapacity()
    {
        return aircrafts.Sum(aircraft => aircraft.GetCargoCapacity());
    }

    public List<Aircraft> SortByFlightRange()
    {
        List<Aircraft> sortedAircrafts = new(aircrafts);
        sortedAircrafts.Sort();
        return sortedAircrafts;
    }

    public List<Aircraft> FindByFuelConsumption(double minFuel, double maxFuel)
    {
        if (minFuel < 0 || maxFuel < minFuel)
        {
            throw new ArgumentException("Некорректный диапазон расхода топлива.");
        }

        return aircrafts
            .Where(aircraft => aircraft.FuelConsumption >= minFuel &&
                               aircraft.FuelConsumption <= maxFuel)
            .ToList();
    }

    public void PrintAllAircraft()
    {
        if (aircrafts.Count == 0)
        {
            Console.WriteLine("Авиапарк пуст.");
            return;
        }

        foreach (Aircraft aircraft in aircrafts)
        {
            aircraft.PrintInfo();
        }
    }
}
