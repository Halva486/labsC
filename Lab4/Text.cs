using System.Collections.Generic;
namespace TextTokenizationLab
{
    public class Text
    {
        public List<string> OriginalLines { get; set; } = new List<string>();
        public Dictionary<string, (int Count, SortedSet<int> Lines)> BuildConcordance()
        {
            var result = new Dictionary<string, (int Count, SortedSet<int> Lines)>();
            for (int i = 0; i < OriginalLines.Count; i++)
            {
                foreach (string word in OriginalLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!result.ContainsKey(word)) result[word] = (0, new SortedSet<int>());
                    var item = result[word];
                    item.Count++;
                    item.Lines.Add(i + 1);
                    result[word] = item;
                }
            }
            return result;
        }
    }
}