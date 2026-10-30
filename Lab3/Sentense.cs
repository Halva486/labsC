using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace TextTokenizationLab
{
    [Serializable]
    public class Sentence
    {
        [XmlElement("word")]
        public List<Word> Words { get; set; } = new List<Word>();

        [XmlIgnore]
        public List<IToken> Tokens { get; set; } = new List<IToken>();

        public Sentence() { }

        [XmlIgnore]
        public bool IsQuestion => Tokens.LastOrDefault()?.Value == "?";

        [XmlIgnore]
        public int WordCount => Words.Count;

        [XmlIgnore]
        public int Length => string.Join("", Tokens.Select(t => t.Value)).Length;

        public void SyncWords()
        {
            Words = Tokens.OfType<Word>().ToList();
        }

        public override string ToString()
        {
            var result = new System.Text.StringBuilder();
            string previous = "";
            for (int i = 0; i < Tokens.Count; i++)
            {
                var token = Tokens[i];
                string value = token.Value;
                if (string.IsNullOrEmpty(value)) continue;
                bool closing = token is Punctuation && ".,!?;:)]}".Contains(value);
                bool previousOpening = "([{".Contains(previous);
                bool attached = previous == "-" || value == "-" || previous == "'" || value == "'";
                if (result.Length > 0 && !closing && !previousOpening && !attached)
                    result.Append(' ');
                result.Append(value);
                previous = value;
            }
            return result.ToString();
        }
    }
}
