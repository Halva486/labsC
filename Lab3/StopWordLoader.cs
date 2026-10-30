using System;
using System.Collections.Generic;
using System.IO;

namespace TextTokenizationLab
{
    public static class StopWordLoader
    {
        public static HashSet<string> Load(params string[] paths)
        {
            ArgumentNullException.ThrowIfNull(paths);
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var path in paths)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("Не найден файл стоп-слов.", path);
                foreach (string line in File.ReadLines(path))
                {
                    string word = line.Trim();
                    if (word.Length > 0) words.Add(word);
                }
            }
            return words;
        }
        public static HashSet<string> LoadDefault() =>
            Load(Path.Combine(AppContext.BaseDirectory, "stopwords_ru.txt"),
                 Path.Combine(AppContext.BaseDirectory, "stopwords_en.txt"));
    }
}
