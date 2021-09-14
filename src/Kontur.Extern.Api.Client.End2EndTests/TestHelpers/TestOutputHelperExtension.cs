using Xunit.Abstractions;

namespace Kontur.Extern.Api.Client.End2EndTests.TestHelpers
{
    internal static class TestOutputHelperExtension
    {
        public static void PrintSeparator(this ITestOutputHelper output, string title)
        {
            output.WriteLine(new string('-', 10) + $" {title} " + new string('-', 10));
        }
    }
}