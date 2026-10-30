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
            try { return StopWordLoader.LoadDefault(); }
            catch (IOException ex)
            {
                Console.WriteLine($"Ошибка загрузки стоп-слов: {ex.Message}");
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }
    }
}
