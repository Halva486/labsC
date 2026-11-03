using System;
using System.Collections.Generic;
using System.Linq;

namespace TextTokenizationLab
{
    public class Text
    {
        public List<Sentence> Sentences { get; set; } = new List<Sentence>();
        public List<string> OriginalLines { get; set; } = new List<string>();

        public Dictionary<string, (int Count, SortedSet<int> Lines)> BuildConcordance()
        {
            var concordance = new Dictionary<string, (int Count, SortedSet<int> Lines)>();

            for (int i = 0; i < OriginalLines.Count; i++)
            {
                string line = OriginalLines[i];
                var matches = System.Text.RegularExpressions.Regex.Matches(
                    line, AppConstants.TokenRegex);

                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    if (!match.Groups[1].Success) continue;

                    string word = match.Value.ToLowerInvariant();

                    if (!concordance.ContainsKey(word))
                        concordance[word] = (0, new SortedSet<int>());

                    var info = concordance[word];
                    info.Count++;
                    info.Lines.Add(i + 1);
                    concordance[word] = info;
                }
            }
            return concordance;
        }

        public void PrintConcordance()
        {
            Console.WriteLine("\n--- PROJECT 04: CONCORDANCE ---");
            var concordance = BuildConcordance();
            foreach (var item in concordance.OrderBy(x => x.Key))
            {
                string lines = string.Join(" ", item.Value.Lines);
                Console.WriteLine($"{item.Key.PadRight(20, '.')} {item.Value.Count}: {lines}");
            }
        }
    }
}
