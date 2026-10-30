using System;
using System.IO;

namespace TextTokenizationLab
{
    public static class ConsoleMenu
    {
        public static void Run()
        {
            Text? text = null;
            while (true)
            {
                Console.WriteLine("\n1. Ввести текст\n2. Показать текст\n3. Сортировать по числу слов");
                Console.WriteLine("4. Сортировать по длине\n5. Найти слова в вопросах\n6. Удалить слова на согласную");
                Console.WriteLine("7. Заменить слова\n8. Удалить стоп-слова\n9. Экспорт XML\n0. Выход");
                Console.Write("Выбор: ");
                var choice = Console.ReadLine();
                if (choice == "0" || choice == null) return;
                if (choice == "1")
                {
                    Console.Write("Введите текст: ");
                    text = TextParser.Parse(Console.ReadLine() ?? "");
                    Console.WriteLine($"Предложений: {text.Sentences.Count}");
                    continue;
                }
                if (text == null || text.Sentences.Count == 0)
                {
                    Console.WriteLine("Сначала введите непустой текст.");
                    continue;
                }
                try
                {
                    switch (choice)
                    {
                        case "2": text.PrintText(); break;
                        case "3": text.PrintOrderByWordCount(); break;
                        case "4": text.PrintOrderByLength(); break;
                        case "5": text.FindWordsInQuestions(ReadPositive("Длина слова: ")); break;
                        case "6": text.RemoveWordsStartingWithConsonant(ReadPositive("Длина слова: ")); text.PrintText(); break;
                        case "7":
                            int index = ReadPositive("Номер предложения (от 1): ") - 1;
                            int length = ReadPositive("Длина слова: ");
                            Console.Write("Текст замены: ");
                            text.ReplaceWordsInSentence(index, length, Console.ReadLine() ?? "");
                            text.PrintText();
                            break;
                        case "8": text.RemoveStopWords(StopWordLoader.LoadDefault()); text.PrintText(); break;
                        case "9":
                            Console.Write("Путь XML (Enter для exported_text.xml): ");
                            string path = Console.ReadLine() ?? "";
                            text.ExportToXml(string.IsNullOrWhiteSpace(path) ? "exported_text.xml" : path);
                            break;
                        default: Console.WriteLine("Неизвестный пункт меню."); break;
                    }
                }
                catch (Exception ex) when (ex is ArgumentException || ex is IOException)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private static int ReadPositive(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                    return result;
                Console.WriteLine("Введите положительное целое число.");
            }
        }
    }
}
