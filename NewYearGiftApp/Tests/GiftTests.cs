using System.Collections.Generic;
using Xunit;
using NewYearGiftApp;

namespace NewYearGiftApp.Tests
{
    public class GiftTests
    {
        [Fact]
        public void AddSweet_ShouldIncreaseSweetsCount()
        {
            var gift = new NewYearGift();
            var candy = new ChocolateCandy("Аленка", 20, 55.0, "Молочный");

            gift.AddSweet(candy);

            Assert.Single(gift.Sweets);
            Assert.Equal("Аленка", gift.Sweets[0].Name);
        }

        [Fact]
        public void GetTotalWeight_ShouldReturnSumOfAllSweetsWeights()
        {
            var gift = new NewYearGift();
            gift.AddSweet(new ChocolateCandy("Мишка", 15, 52.5, "Темный"));
            gift.AddSweet(new CaramelCandy("Барбарис", 8, 78.0, "Барбарис"));
            gift.AddSweet(new ChocolateCandy("Аленка", 20, 55.0, "Молочный"));

            double totalWeight = gift.GetTotalWeight();

            Assert.Equal(43.0, totalWeight);
        }

        [Fact]
        public void GetTotalWeight_EmptyGift_ShouldReturnZero()
        {
            var gift = new NewYearGift();

            double totalWeight = gift.GetTotalWeight();

            Assert.Equal(0.0, totalWeight);
        }

        [Fact]
        public void SortByWeight_ShouldSortSweetsInAscendingOrder()
        {
            var gift = new NewYearGift();
            gift.AddSweet(new ChocolateCandy("Тяжелая", 25, 50.0, "Горький"));
            gift.AddSweet(new CaramelCandy("Легкая", 7, 80.0, "Груша"));
            gift.AddSweet(new ChocolateCandy("Средняя", 15, 55.0, "Молочный"));

            gift.SortByWeight();

            Assert.Equal("Легкая", gift.Sweets[0].Name);
            Assert.Equal("Средняя", gift.Sweets[1].Name);
            Assert.Equal("Тяжелая", gift.Sweets[2].Name);
        }

        [Fact]
        public void FindBySugarRange_ShouldReturnOnlyMatchingSweets()
        {
            var gift = new NewYearGift();
            gift.AddSweet(new ChocolateCandy("Мишка", 15, 52.5, "Темный"));
            gift.AddSweet(new CaramelCandy("Барбарис", 8, 78.0, "Барбарис"));
            gift.AddSweet(new ChocolateCandy("Аленка", 20, 55.0, "Молочный"));
            gift.AddSweet(new CaramelCandy("Дюшес", 7, 81.5, "Груша"));

            List<Sweet> result = gift.FindBySugarRange(50.0, 60.0);

            Assert.Equal(2, result.Count);
            Assert.Contains(result, s => s.Name == "Мишка");
            Assert.Contains(result, s => s.Name == "Аленка");
        }

        [Fact]
        public void GetTypeInfo_Polymorphism_ShouldReturnCorrectDescriptions()
        {
            Sweet chocolate = new ChocolateCandy("Трюфель", 18, 48.0, "Горький");
            Sweet caramel = new CaramelCandy("Взлетная", 6, 82.0, "Лимон");

            Assert.Equal("Шоколадная (Горький)", chocolate.GetTypeInfo());
            Assert.Equal("Карамель (Лимон)", caramel.GetTypeInfo());
        }
    }
}
