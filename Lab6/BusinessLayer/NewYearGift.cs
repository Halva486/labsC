namespace BusinessLayer;
public class NewYearGift : IGift
{
    private readonly List<Sweet> sweets = new();
    public IReadOnlyList<Sweet> Sweets => sweets.AsReadOnly();
    public void AddSweet(Sweet sweet)
    {
        ArgumentNullException.ThrowIfNull(sweet);
        sweets.Add(sweet);
    }
    public double GetTotalWeight() => sweets.Sum(x=>x.Weight);
    public void SortByWeight() => sweets.Sort();
    public List<Sweet> FindBySugarRange(double minSugar,double maxSugar)
    {
        if (!double.IsFinite(minSugar) || !double.IsFinite(maxSugar) || minSugar<0 || maxSugar>100 || minSugar>maxSugar)
            throw new ArgumentException("Некорректный диапазон содержания сахара (0–100%).");
        return sweets.Where(x=>x.SugarContent>=minSugar && x.SugarContent<=maxSugar).ToList();
    }
}