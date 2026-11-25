using System;

namespace NewYearGiftApp
{
    public abstract class Sweet : IComparable<Sweet>
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public double SugarContent { get; set; }

        protected Sweet(string name, double weight, double sugarContent)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название сладости не может быть пустым.", nameof(name));

            if (double.IsNaN(weight) || double.IsInfinity(weight) || weight <= 0)
                throw new ArgumentOutOfRangeException(nameof(weight), "Вес должен быть положительным числом.");

            if (double.IsNaN(sugarContent) || double.IsInfinity(sugarContent) || sugarContent < 0 || sugarContent > 100)
                throw new ArgumentOutOfRangeException(nameof(sugarContent), "Содержание сахара должно быть от 0 до 100%.");

            Name = name;
            Weight = weight;
            SugarContent = sugarContent;
        }

        public abstract string GetTypeInfo();

        public int CompareTo(Sweet? other)
        {
            if (other == null) return 1;
            return Weight.CompareTo(other.Weight);
        }

        public override string ToString()
        {
            return $"{Name,-16} | Вес: {Weight,3} г | Сахар: {SugarContent,4}% | {GetTypeInfo()}";
        }
    }
}
