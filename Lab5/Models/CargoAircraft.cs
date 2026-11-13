namespace lab5.Models;

/// <summary>
/// Грузовой самолёт.
/// </summary>
public class CargoAircraft : Aircraft
{
    public double CargoCapacity { get; }

    public CargoAircraft(
        string model,
        double flightRange,
        double fuelConsumption,
        double cargoCapacity)
        : base(model, flightRange, fuelConsumption)
    {
        if (cargoCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cargoCapacity),
                "Грузоподъёмность должна быть положительной.");
        }

        CargoCapacity = cargoCapacity;
    }

    public override int GetPassengerCapacity() => 0;

    public override double GetCargoCapacity() => CargoCapacity;

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"Грузовой самолёт: {Model}, грузоподъёмность: {CargoCapacity} т, " +
            $"дальность: {FlightRange} км, расход: {FuelConsumption} л/ч");
    }
}
