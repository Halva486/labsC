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
