namespace lab5.Models;

/// <summary>
/// Базовый абстрактный класс воздушного судна.
/// </summary>
public abstract class Aircraft
{
    public string Model { get; }
    public double FlightRange { get; }
    public double FuelConsumption { get; }

    protected Aircraft(string model, double flightRange, double fuelConsumption)
    {
        if (string.IsNullOrWhiteSpace(model))
        {
            throw new ArgumentException("Название модели не может быть пустым.", nameof(model));
        }

        if (flightRange <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(flightRange),
                "Дальность полёта должна быть положительной.");
        }

        if (fuelConsumption <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(fuelConsumption),
                "Расход топлива должен быть положительным.");
        }

        Model = model;
        FlightRange = flightRange;
        FuelConsumption = fuelConsumption;
    }

    public abstract int GetPassengerCapacity();

    public abstract double GetCargoCapacity();

    public virtual void PrintInfo()
    {
        Console.WriteLine(
            $"{Model}: дальность — {FlightRange} км, расход топлива — {FuelConsumption} л/ч, " +
            $"пассажиры — {GetPassengerCapacity()}, груз — {GetCargoCapacity()} т");
    }
}
