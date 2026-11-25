using System.Collections.Generic;

namespace NewYearGiftApp
{
    public interface IGift
    {
        void AddSweet(Sweet sweet);
        double GetTotalWeight();
        void SortByWeight();
        List<Sweet> FindBySugarRange(double minSugar, double maxSugar);
    }
}
