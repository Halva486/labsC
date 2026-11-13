using lab5.Models;
using lab5.Services;

namespace lab5;

/// <summary>
/// Простые автоматические проверки основных возможностей проекта.
/// </summary>
public static class Tests
{
    private static int passed;
    private static int failed;

    public static void RunAll()
    {
        passed = 0;
        failed = 0;

        TestPassengerAircraft();
        TestCargoAircraft();
        TestMixedAircraft();
        TestTotalCapacity();
        TestSorting();
        TestFuelSearch();
        TestEmptyAirline();
        TestValidation();
        TestPolymorphism();

        Console.WriteLine($"Результат: успешно — {passed}, ошибок — {failed}.");
    }

    private static void Check(string description, bool condition)
    {
        if (condition)
        {
            passed++;
            Console.WriteLine($"[OK] {description}");
        }
        else
        {
            failed++;
            Console.WriteLine($"[ОШИБКА] {description}");
        }
    }

    private static void TestPassengerAircraft()
    {
        var aircraft = new PassengerAircraft("Boeing 737", 5600, 2500, 180);
        Check("Пассажирский самолёт: количество мест", aircraft.GetPassengerCapacity() == 180);
        Check("Пассажирский самолёт: грузоподъёмность равна нулю", aircraft.GetCargoCapacity() == 0);
        Check("Пассажирский самолёт: модель сохранена", aircraft.Model == "Boeing 737");
    }

    private static void TestCargoAircraft()
    {
        var aircraft = new CargoAircraft("Ан-124", 4800, 12000, 120);
        Check("Грузовой самолёт: пассажировместимость равна нулю", aircraft.GetPassengerCapacity() == 0);
        Check("Грузовой самолёт: грузоподъёмность", aircraft.GetCargoCapacity() == 120);
    }

    private static void TestMixedAircraft()
    {
        var aircraft = new MixedAircraft("Boeing 747 Combi", 9800, 10500, 250, 40);
        Check("Комбинированный самолёт: количество мест", aircraft.GetPassengerCapacity() == 250);
        Check("Комбинированный самолёт: грузоподъёмность", aircraft.GetCargoCapacity() == 40);
    }

    private static Airline CreateTestAirline()
    {
        var airline = new Airline("Тестовая авиакомпания");
        airline.AddAircraft(new PassengerAircraft("Boeing 737", 5600, 2500, 180));
        airline.AddAircraft(new PassengerAircraft("Airbus A320", 6100, 2700, 150));
        airline.AddAircraft(new CargoAircraft("Ан-124 Руслан", 4800, 12000, 120));
        airline.AddAircraft(new CargoAircraft("Boeing 747F", 8200, 10500, 110));
        airline.AddAircraft(new MixedAircraft("Boeing 747 Combi", 9800, 10500, 250, 40));
        return airline;
    }

    private static void TestTotalCapacity()
    {
        Airline airline = CreateTestAirline();
        Check("Подсчёт общей пассажировместимости", airline.GetTotalPassengerCapacity() == 580);
        Check("Подсчёт общей грузоподъёмности", airline.GetTotalCargoCapacity() == 270);
    }

    private static void TestSorting()
    {
        List<Aircraft> sorted = CreateTestAirline().SortByFlightRange();
        Check("Сортировка: первым стал самолёт с минимальной дальностью",
            sorted.First().Model == "Ан-124 Руслан");
        Check("Сортировка: последним стал самолёт с максимальной дальностью",
            sorted.Last().Model == "Boeing 747 Combi");
        Check("Сортировка: дальность полёта упорядочена",
            sorted.Zip(sorted.Skip(1), (first, second) =>
                first.FlightRange <= second.FlightRange).All(isOrdered => isOrdered));
    }

    private static void TestFuelSearch()
    {
        List<Aircraft> found = CreateTestAirline().FindByFuelConsumption(2000, 5000);
        Check("Поиск по расходу топлива: найдено два самолёта", found.Count == 2);
        Check("Поиск по расходу топлива: правильные модели",
            found.Any(aircraft => aircraft.Model == "Boeing 737") &&
            found.Any(aircraft => aircraft.Model == "Airbus A320"));
    }

    private static void TestEmptyAirline()
    {
        var airline = new Airline("Пустая авиакомпания");
        Check("Пустой авиапарк: пассажировместимость равна нулю",
            airline.GetTotalPassengerCapacity() == 0);
        Check("Пустой авиапарк: грузоподъёмность равна нулю",
            airline.GetTotalCargoCapacity() == 0);
        Check("Пустой авиапарк: сортировка возвращает пустой список",
            airline.SortByFlightRange().Count == 0);
    }

    private static void TestValidation()
    {
        bool invalidModelThrown = false;
        bool invalidCapacityThrown = false;

        try
        {
            _ = new PassengerAircraft("", 5000, 2000, 100);
        }
        catch (ArgumentException)
        {
            invalidModelThrown = true;
        }

        try
        {
            _ = new PassengerAircraft("Test", 5000, 2000, 0);
        }
        catch (ArgumentOutOfRangeException)
        {
            invalidCapacityThrown = true;
        }

        Check("Проверка валидации: пустая модель отклоняется", invalidModelThrown);
        Check("Проверка валидации: нулевая вместимость отклоняется", invalidCapacityThrown);
    }

    private static void TestPolymorphism()
    {
        List<Aircraft> aircrafts =
        [
            new PassengerAircraft("Passenger", 5000, 2000, 100),
            new CargoAircraft("Cargo", 6000, 3000, 50),
            new MixedAircraft("Mixed", 7000, 4000, 80, 20)
        ];

        Check("Полиморфизм: базовый тип хранит разные типы самолётов",
            aircrafts.Count == 3 &&
            aircrafts[0] is PassengerAircraft &&
            aircrafts[1] is CargoAircraft &&
            aircrafts[2] is MixedAircraft);

        Check("Полиморфизм: виртуальные методы возвращают разные значения",
            aircrafts.Sum(aircraft => aircraft.GetPassengerCapacity()) == 180 &&
            aircrafts.Sum(aircraft => aircraft.GetCargoCapacity()) == 70);
    }
}
