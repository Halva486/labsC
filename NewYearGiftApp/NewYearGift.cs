using System;
using System.Collections.Generic;
using System.Linq;

namespace NewYearGiftApp
{
    public class NewYearGift : IGift
    {
        public List<Sweet> Sweets { get; } = new List<Sweet>();

        public void AddSweet(Sweet sweet)
        {
            if (sweet == null)
                throw new ArgumentNullException(nameof(sweet));

            Sweets.Add(sweet);
        }

        public double GetTotalWeight()
        {
            return Sweets.Sum(s => s.Weight);
        }

        public string GetGiftInfo()
        {
            return $"В подарке {Sweets.Count} конфет(ы). Общий вес: {GetTotalWeight():0.##} г.";
        }

        public void SortByWeight()
        {
            Sweets.Sort();
        }

        public void SortBySugarContent()
        {
            Sweets.Sort((first, second) =>
                first.SugarContent.CompareTo(second.SugarContent));
        }

        public List<Sweet> FindBySugarRange(double minSugar, double maxSugar)
        {
            ValidateSugarRange(minSugar, maxSugar);

            return Sweets
                .Where(s => s.SugarContent >= minSugar && s.SugarContent <= maxSugar)
                .ToList();
        }

        private static void ValidateSugarRange(double minSugar, double maxSugar)
        {
            if (double.IsNaN(minSugar) || double.IsInfinity(minSugar) || minSugar < 0 || minSugar > 100)
                throw new ArgumentOutOfRangeException(nameof(minSugar),
                    "Минимальное содержание сахара должно находиться в диапазоне от 0 до 100%.");

            if (double.IsNaN(maxSugar) || double.IsInfinity(maxSugar) || maxSugar < 0 || maxSugar > 100)
                throw new ArgumentOutOfRangeException(nameof(maxSugar),
                    "Максимальное содержание сахара должно находиться в диапазоне от 0 до 100%.");

            if (minSugar > maxSugar)
                throw new ArgumentException("Минимальное содержание сахара не может быть больше максимального.");
        }
    }
}
