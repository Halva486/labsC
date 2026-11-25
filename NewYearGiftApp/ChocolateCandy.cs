namespace NewYearGiftApp
{
    public class ChocolateCandy : Sweet
    {
        public string ChocolateType { get; set; }

        public ChocolateCandy(string name, double weight, double sugarContent, string chocolateType)
            : base(name, weight, sugarContent)
        {
            ChocolateType = chocolateType;
        }

        public override string GetTypeInfo()
        {
            return $"Шоколадная ({ChocolateType})";
        }
    }
}
