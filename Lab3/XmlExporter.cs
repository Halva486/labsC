using System;
using System.Linq;
using System.Xml.Linq;

namespace TextTokenizationLab
{
    public static class XmlExporter
    {
        public static void Export(Text text, string filePath)
        {
            ArgumentNullException.ThrowIfNull(text);
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Укажите путь к XML.", nameof(filePath));

            var xml = new XDocument(new XElement("text",
                text.Sentences.Select(sentence => new XElement("sentence",
                    sentence.Tokens.Select(token => new XElement("token",
                        new XAttribute("type", token is Word ? "word" : "punctuation"),
                        token.Value))))));
            xml.Save(filePath);
        }
    }
}
