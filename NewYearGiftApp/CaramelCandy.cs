namespace NewYearGiftApp
{
    public class CaramelCandy : Sweet
    {
        public string Flavor { get; set; }

        public CaramelCandy(string name, double weight, double sugarContent, string flavor)
            : base(name, weight, sugarContent)
        {
            Flavor = flavor;
        }

        public override string GetTypeInfo()
        {
            return $"Карамель ({Flavor})";
        }
    }
}
