namespace TextTokenizationLab
{
    public static class AppConstants
    {
        public const string Consonants = "bcdfghjklmnpqrstvwxyzбвгджзйклмнпрстфхцчшщ";

        public const string SentenceRegex = @"[^.!?]+([.!?]+|\s*$)";

        public const string TokenRegex = @"([a-zA-Z0-9а-яА-ЯёЁ'-]+)|([.!?,-:;\""()])";
    }
}