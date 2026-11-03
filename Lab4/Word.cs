using System;
using System.Xml.Serialization;

namespace TextTokenizationLab
{
    [Serializable]
    public class Word : IToken
    {
        [XmlText]
        public string Value { get; set; }

        public Word() { }
        public Word(string value) { Value = value; }

        public bool StartsWithConsonant()
        {
            if (string.IsNullOrEmpty(Value)) return false;
            return AppConstants.Consonants.Contains(char.ToLower(Value[0]));
        }

        public override string ToString() => Value;
    }
}
