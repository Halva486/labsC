using lab5.Models;
using lab5.Services;

namespace lab5;

internal static class Program
{
    private static void Main()
    {
        var airline = new Airline("Аэрофлот");

        airline.AddAircraft(new PassengerAircraft("Boeing 737", 5600, 2500, 180));
        airline.AddAircraft(new PassengerAircraft("Airbus A320", 6100, 2700, 150));
        airline.AddAircraft(new CargoAircraft("Ан-124 Руслан", 4800, 12000, 120));
        airline.AddAircraft(new CargoAircraft("Boeing 747F", 8200, 10500, 110));
        airline.AddAircraft(new MixedAircraft("Boeing 747 Combi", 9800, 10500, 250, 40));

        Console.WriteLine($"Авиакомпания: {airline.Name}");
        Console.WriteLine(new string('=', 55));

        Console.WriteLine("\nСамолёты авиакомпании:");
        airline.PrintAllAircraft();

        Console.WriteLine("\nОбщая пассажировместимость: " +
                          $"{airline.GetTotalPassengerCapacity()} пассажиров");
        Console.WriteLine("Общая грузоподъёмность: " +
                          $"{airline.GetTotalCargoCapacity()} тонн");

        Console.WriteLine("\nСамолёты, отсортированные по дальности полёта:");
        foreach (Aircraft aircraft in airline.SortByFlightRange())
        {
            aircraft.PrintInfo();
        }

        Console.WriteLine("\nПоиск самолётов с расходом топлива от 2000 до 5000:");
        List<Aircraft> foundAircraft =
            airline.FindByFuelConsumption(2000, 5000);

        if (foundAircraft.Count == 0)
        {
            Console.WriteLine("Самолёты не найдены.");
        }
        else
        {
            foreach (Aircraft aircraft in foundAircraft)
            {
                aircraft.PrintInfo();
            }
        }

        Console.WriteLine("\nПроверка тестов:");
        Tests.RunAll();
    }
}
