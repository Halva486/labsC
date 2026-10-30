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

        public Word(string value)
        {
            Value = value;
        }
        
        public bool StartsWithConsonant()
        {
            if (string.IsNullOrEmpty(Value)) return false;

            char firstChar = char.ToLower(Value[0]);

            return AppConstants.Consonants.Contains(firstChar);
        }


        public override string ToString() => Value;
    }
}