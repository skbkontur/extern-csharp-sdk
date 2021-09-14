namespace Kontur.Extern.Api.Client.Testing.Helpers
{
    public static class StringExtension
    {
        public static string ToQuoted(this string value) => $"\"{value}\"";
    }
}