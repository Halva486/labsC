using System;
namespace BusinessLayer;
public abstract class Sweet : IComparable<Sweet>
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double SugarContent { get; set; }
    protected Sweet(string name, double weight, double sugarContent)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название не может быть пустым.");
        if (!double.IsFinite(weight) || weight <= 0) throw new ArgumentOutOfRangeException(nameof(weight));
        if (!double.IsFinite(sugarContent) || sugarContent < 0 || sugarContent > 100) throw new ArgumentOutOfRangeException(nameof(sugarContent));
        Name=name; Weight=weight; SugarContent=sugarContent;
    }
    public abstract string GetTypeInfo();
    public int CompareTo(Sweet? other) => other is null ? 1 : Weight.CompareTo(other.Weight);
    public override string ToString() => $"{Name} | {Weight} г | сахар: {SugarContent}% | {GetTypeInfo()}";
}