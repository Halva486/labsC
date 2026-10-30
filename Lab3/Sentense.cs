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
            for (int i = 0; i < Tokens.Count; i++)
            {
                var current = Tokens[i];
                if (i > 0 && current is Word && !(Tokens[i - 1] is Punctuation && (Tokens[i - 1].Value == "'" || Tokens[i - 1].Value == "-")))
                {
                    result.Append(" ");
                }
                result.Append(current.Value);
            }
            return result.ToString();
        }
    }
}