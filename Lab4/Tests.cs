using System;
using System.Linq;

namespace TextTokenizationLab
{
    public static class Lab4Tests
    {
        private static int passed;
        private static int failed;

        public static int RunAll()
        {
            passed = 0;
            failed = 0;
            Console.WriteLine("=== TESTS PROJECT 04 ===\n");

            Run("Word count", TestWordCount);
            Run("Line numbers", TestLineNumbers);
            Run("Unique line numbers", TestUniqueLines);
            Run("Case insensitive", TestCaseInsensitive);
            Run("Punctuation ignored", TestPunctuation);
            Run("Empty text", TestEmptyText);
            Run("Alphabetical order", TestAlphabeticalOrder);

            Console.WriteLine($"\nRESULT: passed {passed}, failed {failed}, total {passed + failed}");
            return failed == 0 ? 0 : 1;
        }

        private static void Run(string name, Action test)
        {
            try { test(); passed++; Console.WriteLine($"[OK] {name}"); }
            catch (Exception ex) { failed++; Console.WriteLine($"[FAIL] {name}: {ex.Message}"); }
        }

        private static void Check(bool condition, string message)
        {
            if (!condition) throw new Exception(message);
        }

        private static void TestWordCount()
        {
            var result = TextParser.Parse("cat dog cat").BuildConcordance();
            Check(result["cat"].Count == 2, "cat should occur twice");
            Check(result["dog"].Count == 1, "dog should occur once");
        }

        private static void TestLineNumbers()
        {
            var result = TextParser.Parse("cat dog\ndog bird\ncat").BuildConcordance();
            Check(result["cat"].Lines.SequenceEqual(new[] { 1, 3 }), "wrong cat lines");
            Check(result["dog"].Lines.SequenceEqual(new[] { 1, 2 }), "wrong dog lines");
        }

        private static void TestUniqueLines()
        {
            var result = TextParser.Parse("cat cat cat\ncat").BuildConcordance();
            Check(result["cat"].Count == 4, "cat should occur four times");
            Check(result["cat"].Lines.SequenceEqual(new[] { 1, 2 }), "line numbers should be unique");
        }

        private static void TestCaseInsensitive()
        {
            var result = TextParser.Parse("Cat CAT cat").BuildConcordance();
            Check(result.ContainsKey("cat") && result["cat"].Count == 3, "case variants should be combined");
        }

        private static void TestPunctuation()
        {
            var result = TextParser.Parse("cat, dog! cat?").BuildConcordance();
            Check(result.Count == 2, "punctuation must not become words");
            Check(result["cat"].Count == 2, "cat should occur twice");
        }

        private static void TestEmptyText()
        {
            var result = TextParser.Parse("").BuildConcordance();
            Check(result.Count == 0, "empty input should produce empty concordance");
        }

        private static void TestAlphabeticalOrder()
        {
            var result = TextParser.Parse("zebra apple banana").BuildConcordance();
            var words = result.Keys.OrderBy(x => x).ToArray();
            Check(words.SequenceEqual(new[] { "apple", "banana", "zebra" }), "wrong alphabetical order");
        }
    }
}
