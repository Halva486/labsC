using lab5.Models;

namespace lab5.Interfaces;

/// <summary>
/// Интерфейс поиска самолётов по расходу топлива.
/// </summary>
public interface IAircraftSearch
{
    List<Aircraft> FindByFuelConsumption(double minFuel, double maxFuel);
}
