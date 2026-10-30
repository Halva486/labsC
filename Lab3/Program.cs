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
           // Lab3Tests.RunAll();
           // return;
            Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА: ТОКЕНИЗАЦИЯ ТЕКСТА ===");
            Console.WriteLine("Введите ваш произвольный текст для анализа и нажмите Enter:");
            Console.Write("> ");

            string sourceText = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(sourceText))
            {
                Console.WriteLine("Ошибка: Текст пустой! Перезапустите программу.");
                return;
            }

            Console.WriteLine("\n[Система]: Начинаем обработку текста...");

            Text myText = TextParser.Parse(sourceText);
            myText.PrintText();

            myText.PrintOrderByWordCount();

            myText.PrintOrderByLength();

            myText.FindWordsInQuestions(4);

            myText.ReplaceWordsInSentence(0, 4, "[ЗАМЕНА]");
            Console.WriteLine("\nРезультат после замены слов (Пункт 5):");
            myText.PrintText();

            HashSet<string> stopWords = LoadStopWords();
            myText.RemoveStopWords(stopWords);
            Console.WriteLine("\nРезультат после удаления стоп-слов (Пункт 6):");
            myText.PrintText();

            myText.RemoveWordsStartingWithConsonant(5);
            Console.WriteLine("\nРезультат после удаления слов на согласную (Пункт 4):");
            myText.PrintText();

            string xmlPath = "exported_text.xml";
            myText.ExportToXml(xmlPath);

            Console.WriteLine("\n=== Лабораторная работа успешно выполнена! ===");
            Console.WriteLine("Проверьте файл 'exported_text.xml' в папке проекта.");
            Console.ReadKey();
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