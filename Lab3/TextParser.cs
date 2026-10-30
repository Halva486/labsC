using System;
using System.Text.RegularExpressions;

namespace TextTokenizationLab
{
    public static class TextParser
    {
        public static Text Parse(string input)
        {
            Text textObj = new Text();
            if (string.IsNullOrWhiteSpace(input)) return textObj;

            var sentenceMatches = Regex.Matches(input, AppConstants.SentenceRegex);

            foreach (Match sMatch in sentenceMatches)
            {
                string sText = sMatch.Value.Trim();
                if (string.IsNullOrEmpty(sText)) continue;

                Sentence sentence = new Sentence();

                var tokenMatches = Regex.Matches(sText, AppConstants.TokenRegex);

                foreach (Match tMatch in tokenMatches)
                {
                    string tokenValue = tMatch.Value;

                    if (tMatch.Groups[1].Success)
                    {
                        sentence.Tokens.Add(new Word(tokenValue));
                    }
                    else
                    {
                        sentence.Tokens.Add(new Punctuation(tokenValue));
                    }
                }

                if (sentence.Tokens.Count > 0)
                {
                    sentence.SyncWords();
                    textObj.Sentences.Add(sentence);
                }
            }

            return textObj;
        }
    }
}