using BusinessLayer;
using DataAccessLayer;
namespace ServiceLayer;
public class GiftService
{
    private readonly GiftRepository repository;
    private NewYearGift gift;
    public GiftService(string filePath="gift.json") { repository=new GiftRepository(filePath); gift=repository.Load(); }
    public void AddSweet(Sweet sweet) { gift.AddSweet(sweet); repository.Save(gift); }
    public double GetTotalWeight()=>gift.GetTotalWeight();
    public void SortByWeight(){gift.SortByWeight();repository.Save(gift);}
    public List<Sweet> FindBySugarRange(double min,double max)=>gift.FindBySugarRange(min,max);
    public IReadOnlyList<Sweet> GetAllSweets()=>gift.Sweets;
    public void SaveGift()=>repository.Save(gift);
}