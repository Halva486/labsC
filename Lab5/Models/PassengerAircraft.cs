namespace lab5.Models;

/// <summary>
/// Пассажирский самолёт.
/// </summary>
public class PassengerAircraft : Aircraft
{
    public int PassengerCapacity { get; }

    public PassengerAircraft(
        string model,
        double flightRange,
        double fuelConsumption,
        int passengerCapacity)
        : base(model, flightRange, fuelConsumption)
    {
        if (passengerCapacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(passengerCapacity),
                "Количество пассажирских мест должно быть положительным.");
        }

        PassengerCapacity = passengerCapacity;
    }

    public override int GetPassengerCapacity() => PassengerCapacity;

    public override double GetCargoCapacity() => 0;

    public override void PrintInfo()
    {
        Console.WriteLine(
            $"Пассажирский самолёт: {Model}, мест: {PassengerCapacity}, " +
            $"дальность: {FlightRange} км, расход: {FuelConsumption} л/ч");
    }
}
