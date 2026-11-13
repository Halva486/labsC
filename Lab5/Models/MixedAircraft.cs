namespace lab5.Models;

/// <summary>
/// Самолёт, предназначенный для перевозки пассажиров и грузов.
/// </summary>
public class MixedAircraft : Aircraft
{
    public int PassengerCapacity { get; }
    public double CargoCapacity { get; }

    public MixedAircraft(
        string model,
        double flightRange,
        double fuelConsumption,
        int passengerCapacity,
        double cargoCapacity)
        : base(model, flightRange, fuelConsumption)
    {
        if (passengerCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(passengerCapacity),
                "Количество пассажирских мест должно быть положительным.");
        }

        if (cargoCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cargoCapacity),
                "Грузоподъёмность должна быть положительной.");
        }

        PassengerCapacity = passengerCapacity;
        CargoCapacity = cargoCapacity;
    }

    public override int GetPassengerCapacity() => PassengerCapacity;

    public override double GetCargoCapacity() => CargoCapacity;

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"Комбинированный самолёт: {Model}, мест: {PassengerCapacity}, " +
            $"грузоподъёмность: {CargoCapacity} т, дальность: {FlightRange} км, " +
            $"расход: {FuelConsumption} л/ч");
    }
}
