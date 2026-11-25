namespace NewYearGiftApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gift = new NewYearGift();

            gift.AddSweet(new ChocolateCandy("Мишка", 15, 52.5, "Темный"));
            gift.AddSweet(new CaramelCandy("Барбарис", 8, 78.0, "Барбарис"));
            gift.AddSweet(new ChocolateCandy("Аленка", 20, 55.0, "Молочный"));
            gift.AddSweet(new CaramelCandy("Дюшес", 7, 81.5, "Груша"));

            Console.WriteLine("Состав новогоднего подарка:");
            foreach (Sweet sweet in gift.Sweets)
                Console.WriteLine(sweet);

            Console.WriteLine($"
{gift.GetGiftInfo()}");

            gift.SortByWeight();
            Console.WriteLine("
Конфеты после сортировки по весу:");
            foreach (Sweet sweet in gift.Sweets)
                Console.WriteLine(sweet);

            gift.SortBySugarContent();
            Console.WriteLine("
Конфеты после сортировки по содержанию сахара:");
            foreach (Sweet sweet in gift.Sweets)
                Console.WriteLine(sweet);

            Console.WriteLine("
Конфеты с содержанием сахара от 50% до 60%:");
            foreach (Sweet sweet in gift.FindBySugarRange(50.0, 60.0))
                Console.WriteLine(sweet);
        }
    }
}
