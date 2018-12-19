namespace APILinkedin
{
    static class Extension
    {
        internal static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

        internal static bool IsNullOrWhiteSpace(this string s) => string.IsNullOrWhiteSpace(s);
    }
}
