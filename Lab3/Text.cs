using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TextTokenizationLab
{
    [Serializable]
    [XmlRoot("text")]
    public class Text
    {
        [XmlElement("sentence")]
        public List<Sentence> Sentences { get; set; } = new List<Sentence>();

        public Text() { }

        public void PrintOrderByWordCount()
        {
            Console.WriteLine("\n--- 1. Сортировка по количеству слов ---");
            var sorted = Sentences.OrderBy(s => s.WordCount).ToList();
            foreach (var s in sorted)
                Console.WriteLine($"[{s.WordCount} слов]: {s}");
        }

        public void PrintOrderByLength()
        {
            Console.WriteLine("\n--- 2. Сортировка по длине предложения (символы) ---");
            var sorted = Sentences.OrderBy(s => s.Length).ToList();
            foreach (var s in sorted)
                Console.WriteLine($"[{s.Length} симв.]: {s}");
        }

        public void FindWordsInQuestions(int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
            Console.WriteLine($"\n--- 3. Слова длины {length} в вопросительных предложениях ---");
            var words = Sentences
                .Where(s => s.IsQuestion)
                .SelectMany(s => s.Words)
                .Where(w => w.Value.Length == length)
                .Select(w => w.Value.ToLower())
                .Distinct();

            foreach (var word in words)
                Console.WriteLine($"- {word}");
        }

        public void RemoveWordsStartingWithConsonant(int length)
        {
            if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));
            Console.WriteLine($"\n--- 4. Удаление слов длины {length}, начинающихся с согласной ---");
            foreach (var s in Sentences)
            {
                s.Tokens.RemoveAll(t => t is Word w && w.Value.Length == length && w.StartsWithConsonant());
                s.SyncWords();
            }
        }

        public void ReplaceWordsInSentence(int sentenceIndex, int wordLength, string subString)
        {
            if (sentenceIndex < 0 || sentenceIndex >= Sentences.Count) throw new ArgumentOutOfRangeException(nameof(sentenceIndex));
            if (wordLength <= 0) throw new ArgumentOutOfRangeException(nameof(wordLength));
            if (string.IsNullOrEmpty(subString)) throw new ArgumentException("Пустая строка замены.", nameof(subString));
            Console.WriteLine($"\n--- 5. Замена в предложении [{sentenceIndex}] слов длины {wordLength} на '{subString}' ---");
            if (sentenceIndex < 0 || sentenceIndex >= Sentences.Count) throw new ArgumentOutOfRangeException(nameof(sentenceIndex));

            var targetSentence = Sentences[sentenceIndex];
            for (int i = 0; i < targetSentence.Tokens.Count; i++)
            {
                if (targetSentence.Tokens[i] is Word w && w.Value.Length == wordLength)
                {
                    targetSentence.Tokens[i] = new Word(subString);
                }
            }
            targetSentence.SyncWords();
        }

        public void RemoveStopWords(HashSet<string> stopWords)
        {
            ArgumentNullException.ThrowIfNull(stopWords);
            Console.WriteLine("\n--- 6. Удаление стоп-слов ---");
            foreach (var s in Sentences)
            {
                s.Tokens.RemoveAll(t => t is Word w && stopWords.Contains(w.Value.ToLower()));
                s.SyncWords();
            }
        }

        public void ExportToXml(string filePath)
        {
            XmlExporter.Export(this, filePath);
            Console.WriteLine($"XML сохранён: {filePath}");
        }

        public void PrintText()
        {
            Console.WriteLine("\nТекущий текст целиком:");
            foreach (var s in Sentences) Console.WriteLine(s);
        }
    }
}