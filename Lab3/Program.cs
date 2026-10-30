using System;
using System.Collections.Generic;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace TextTokenizationLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЛАБОРАТОРНАЯ №3 ===");
            ConsoleMenu.Run();
        }

        static HashSet<string> LoadStopWords()
        {
            HashSet<string> stopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string ruFile = "stopwords_ru.txt";
            string enFile = "stopwords_en.txt";

            if (File.Exists(ruFile))
            {
                foreach (string line in File.ReadAllLines(ruFile))
                {
                    string word = line.Trim();
                    if (!string.IsNullOrEmpty(word)) stopWords.Add(word);
                }
                Console.WriteLine("\n[Успех]: Загружены русские стоп-слова.");
            }

            if (File.Exists(enFile))
            {
                foreach (string line in File.ReadAllLines(enFile))
                {
                    string word = line.Trim();
                    if (!string.IsNullOrEmpty(word)) stopWords.Add(word);
                }
                Console.WriteLine("[Успех]: Загружены английские стоп-слова.");
            }

            return stopWords;
        }
    }
}