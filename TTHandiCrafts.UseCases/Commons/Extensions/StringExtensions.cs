namespace TTHandiCrafts.UseCases.Commons.Extensions
{
    public static class StringExtensions
    {
        public static string ToLowerFirst(this string text)
            => !string.IsNullOrEmpty(text)
                ? $"{char.ToLower(text[0])}{text.Substring(1)}"
                : text;
    }
}