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
            if (minSugar > maxSugar)
                throw new ArgumentException("Минимальное содержание сахара не может быть больше максимального.");

            return Sweets
                .Where(s => s.SugarContent >= minSugar && s.SugarContent <= maxSugar)
                .ToList();
        }
    }
}
