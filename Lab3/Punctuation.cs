using System;

namespace TextTokenizationLab
{
    public class Punctuation : IToken
    {
        public string Value { get; }

        public Punctuation(string value)
        {
            Value = value;
        }

        public override string ToString() => Value;
    }
}