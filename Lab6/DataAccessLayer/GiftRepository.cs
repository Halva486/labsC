using System.Text.Json;
using BusinessLayer;
namespace DataAccessLayer;
public class GiftRepository
{
    private readonly string filePath;
    public GiftRepository(string filePath="gift.json") => this.filePath=filePath;
    private sealed class SweetData
    {
        public string Name {get;set;}="";
        public double Weight {get;set;}
        public double SugarContent {get;set;}
        public string Type {get;set;}="";
        public string Extra {get;set;}="";
    }
    public void Save(NewYearGift gift)
    {
        ArgumentNullException.ThrowIfNull(gift);
        var rows=gift.Sweets.Select(s=>new SweetData {
            Name=s.Name, Weight=s.Weight, SugarContent=s.SugarContent,
            Type=s is ChocolateCandy ? "Chocolate" : "Caramel",
            Extra=s is ChocolateCandy c ? c.ChocolateType : ((CaramelCandy)s).Flavor
        }).ToList();
        File.WriteAllText(filePath,JsonSerializer.Serialize(rows,new JsonSerializerOptions{WriteIndented=true}));
    }
    public NewYearGift Load()
    {
        var gift=new NewYearGift();
        if(!File.Exists(filePath)) return gift;
        var rows=JsonSerializer.Deserialize<List<SweetData>>(File.ReadAllText(filePath)) ?? new();
        foreach(var x in rows)
            gift.AddSweet(x.Type=="Chocolate"
                ? new ChocolateCandy(x.Name,x.Weight,x.SugarContent,x.Extra)
                : new CaramelCandy(x.Name,x.Weight,x.SugarContent,x.Extra));
        return gift;
    }
}