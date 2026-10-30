
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextTokenizationLab
{
    public static class Lab3Tests
    {
        private static int passed;
        private static int failed;

        public static int RunAll()
        {
            passed = 0;
            failed = 0;

            Console.WriteLine("\n=== ТЕСТЫ ЛАБОРАТОРНОЙ №3 ===\n");

            Run("Парсер разбивает текст на предложения", TestParserSentences);
            Run("Парсер создаёт Word и Punctuation", TestParserTokens);
            Run("Word проверяет согласную", TestStartsWithConsonant);
            Run("Свойства Sentence", TestSentenceProperties);
            Run("1. Сортировка по количеству слов", TestSortByWordCount);
            Run("2. Сортировка по длине", TestSortByLength);
            Run("3. Поиск слов в вопросах", TestWordsInQuestions);
            Run("4. Удаление слов на согласную", TestRemoveConsonantWords);
            Run("5. Замена слов", TestReplaceWords);
            Run("6. Удаление стоп-слов", TestRemoveStopWords);
            Run("7. Экспорт в XML", TestXmlExport);
            Run("Вывод текста", TestPrintText);
            Run("Пустой текст", TestEmptyText);
            Run("Несколько знаков препинания", TestMultiplePunctuation);
            Run("Неверный индекс", TestInvalidSentenceIndex);

            Console.WriteLine(
                $"\nИТОГ: успешно {passed}, ошибок {failed}, всего {passed + failed}.");
            return failed == 0 ? 0 : 1;
        }

        private static void Run(string name, Action test)
        {
            try
            {
                test();
                passed++;
                Console.WriteLine($"[OK] {name}");
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"[ОШИБКА] {name}: {ex.Message}");
            }
        }

        private static void Check(bool condition, string message)
        {
            if (!condition)
                throw new Exception(message);
        }

        private static string CaptureOutput(Action action)
        {
            TextWriter original = Console.Out;
            using var buffer = new StringWriter();

            try
            {
                Console.SetOut(buffer);
                action();
                return buffer.ToString();
            }
            finally
            {
                Console.SetOut(original);
            }
        }

        private static void TestParserSentences()
        {
            Text text = TextParser.Parse(
                "Hello! How are you? I am fine.");

            Check(text.Sentences.Count == 3,
                "Ожидалось 3 предложения.");
        }

        private static void TestParserTokens()
        {
            Text text = TextParser.Parse("Hello, world.");
            var tokens = text.Sentences[0].Tokens;

            Check(tokens.Count == 4,
                "Ожидалось 4 токена.");

            Check(tokens[0] is Word &&
                  tokens[0].Value == "Hello",
                  "Первый токен должен быть Hello.");

            Check(tokens[1] is Punctuation &&
                  tokens[1].Value == ",",
                  "Второй токен должен быть запятой.");

            Check(tokens[2] is Word &&
                  tokens[2].Value == "world",
                  "Третий токен должен быть world.");

            Check(tokens[3] is Punctuation &&
                  tokens[3].Value == ".",
                  "Четвёртый токен должен быть точкой.");
        }

        private static void TestStartsWithConsonant()
        {
            Check(new Word("Cat").StartsWithConsonant(),
                "Cat начинается с согласной.");

            Check(!new Word("Apple").StartsWithConsonant(),
                "Apple начинается с гласной.");

            Check(new Word("Привет").StartsWithConsonant(),
                "Привет начинается с согласной.");

            Check(!new Word("").StartsWithConsonant(),
                "Пустое слово должно возвращать false.");
        }

        private static void TestSentenceProperties()
        {
            Sentence s = TextParser.Parse(
                "How are you?").Sentences[0];

            Check(s.WordCount == 3,
                "Должно быть 3 слова.");

            Check(s.IsQuestion,
                "Предложение должно быть вопросительным.");

            Check(s.Words.Count == 3 && s.Tokens.Count == 4,
                "Неверное количество слов или токенов.");

            Check(s.ToString() == "How are you?",
                "Предложение собрано неправильно.");
        }

        private static void TestSortByWordCount()
        {
            Text text = TextParser.Parse(
                "How are you today? Hello! I love programming.");

            string output = CaptureOutput(
                text.PrintOrderByWordCount);

            int first = output.IndexOf("Hello!");
            int second = output.IndexOf("I love programming.");
            int third = output.IndexOf("How are you today?");

            Check(first >= 0 &&
                  first < second &&
                  second < third,
                  "Неверный порядок по количеству слов.");
        }

        private static void TestSortByLength()
        {
            Text text = TextParser.Parse(
                "Programming is fun. Hi! Hello world!");

            string output = CaptureOutput(
                text.PrintOrderByLength);

            int first = output.IndexOf("Hi!");
            int second = output.IndexOf("Hello world!");
            int third = output.IndexOf("Programming is fun.");

            Check(first >= 0 &&
                  first < second &&
                  second < third,
                  "Неверный порядок по длине.");
        }

        private static void TestWordsInQuestions()
        {
            Text text = TextParser.Parse(
                "What is your name? What are you? This word is fine.");

            string output = CaptureOutput(
                () => text.FindWordsInQuestions(4));

            string[] words = output.Split(
                new[] { '\r', '\n' },
                StringSplitOptions.RemoveEmptyEntries)
                .Where(line => line.StartsWith("- "))
                .Select(line => line.Substring(2).Trim())
                .ToArray();

            Check(words.SequenceEqual(
                new[] { "what", "your", "name" }),
                "Неверный список слов в вопросах.");
        }

        private static void TestRemoveConsonantWords()
        {
            Text text = TextParser.Parse("Cat apple dog.");

            CaptureOutput(() =>
                text.RemoveWordsStartingWithConsonant(3));

            Check(text.Sentences[0].ToString() == "apple.",
                "Ожидалось: apple.");

            Check(text.Sentences[0].WordCount == 1,
                "Количество слов должно быть равно 1.");
        }

        private static void TestReplaceWords()
        {
            Text text = TextParser.Parse(
                "I love code. Keep this.");

            CaptureOutput(() =>
                text.ReplaceWordsInSentence(0, 4, "XYZ"));

            Check(text.Sentences[0].ToString() == "I XYZ XYZ.",
                "Ожидалось: I XYZ XYZ.");

            Check(text.Sentences[1].ToString() == "Keep this.",
                "Второе предложение не должно изменяться.");
        }

        private static void TestRemoveStopWords()
        {
            Text text = TextParser.Parse("The cat is here.");

            var stopWords = new HashSet<string>(
                StringComparer.OrdinalIgnoreCase)
            {
                "the",
                "is"
            };

            CaptureOutput(() =>
                text.RemoveStopWords(stopWords));

            Check(text.Sentences[0].ToString() == "cat here.",
                "Ожидалось: cat here.");

            Check(text.Sentences[0].WordCount == 2,
                "Должно остаться 2 слова.");
        }

        private static void TestXmlExport()
        {
            Text text = TextParser.Parse("Hello world.");

            string file = Path.Combine(
                Path.GetTempPath(),
                "lab3_test_" + Guid.NewGuid().ToString("N") + ".xml");

            try
            {
                CaptureOutput(() => text.ExportToXml(file));

                Check(File.Exists(file),
                    "XML-файл не создан.");

                string xml = File.ReadAllText(file);

                Check(
                    xml.Contains("<text") &&
                    xml.Contains("<sentence>") &&
                    xml.Contains("type=\"word\"") &&
                    xml.Contains(">Hello</token>") &&
                    xml.Contains(">world</token>") &&
                    xml.Contains("type=\"punctuation\""),
                    "XML содержит неправильные данные.");
            }
            finally
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
        }

        private static void TestPrintText()
        {
            Text text = TextParser.Parse(
                "Hello! How are you?");

            string output = CaptureOutput(text.PrintText);

            Check(output.Contains("Hello!") &&
                  output.Contains("How are you?"),
                  "Не все предложения выведены.");
        }
        private static void TestEmptyText()
        {
            Check(TextParser.Parse(" ").Sentences.Count == 0,
                "Пустой ввод должен давать ноль предложений.");
        }

        private static void TestMultiplePunctuation()
        {
            Text result = TextParser.Parse("Привет... Как дела?!");
            Check(result.Sentences.Count == 2, "Нужно два предложения.");
            Check(result.Sentences[1].ToString() == "Как дела?!",
                "Не сохранена пунктуация.");
        }

        private static void TestInvalidSentenceIndex()
        {
            bool thrown = false;
            try { TextParser.Parse("Hello.").ReplaceWordsInSentence(3, 3, "ok"); }
            catch (ArgumentOutOfRangeException) { thrown = true; }
            Check(thrown, "Ожидалась ошибка индекса.");
        }
    }
}