namespace otpsystemback.Data.Helpers
{
    public static class Characters
    {
        public static readonly char[] Punctuations = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();
        public static readonly char[] UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public static readonly char[] LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
        public static readonly char[] Numbers = "1234567890".ToCharArray();
    }
}
