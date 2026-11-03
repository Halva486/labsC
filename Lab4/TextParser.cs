namespace TextTokenizationLab
{
    public static class TextParser
    {
        public static Text Parse(string input)
        {
            Text text = new Text();
            if (input == null) return text;
            foreach (string line in input.Split('\n'))
                text.OriginalLines.Add(line.TrimEnd('\r'));
            return text;
        }
    }
}